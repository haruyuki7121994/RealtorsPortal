using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using src.Models;
using System.IO;
using src.Services;

namespace src.Controllers
{
    public class AuthController : Controller
    {
        private readonly ICustomerService services;
        private readonly IEmailService emailService;
        private readonly IPaymentPackageService paymentPackageService;
        public AuthController(ICustomerService services, IEmailService emailService, IPaymentPackageService paymentPackageService)
        {
            this.services = services;
            this.emailService = emailService;
            this.paymentPackageService = paymentPackageService;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                //check username password
                var cus = services.checkLogin(username, password);
                if (cus != null)
                {
                    //check confirm email
                    if (!cus.Is_verified)
                    {
                        ViewBag.error = "Unverified account! Please confirm email or contact admin for help!";
                        return View();
                    }

                    //check active account
                    if (!cus.Is_active)
                    {
                        ViewBag.error = "Account has been blocked! Please contact admin for help!";
                        return View();
                    }

                    HttpContext.Session.SetString("customer", JsonConvert.SerializeObject(cus));
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ViewBag.error = "Invalid Username or Password";
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customers, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //add avatar
                    if (file != null)
                    {
                        var filepath = Path.Combine("wwwroot/images/avatars", file.FileName);
                        var stream = new FileStream(filepath, FileMode.Create);
                        file.CopyToAsync(stream);
                        customers.Image = "images/avatars" + file.FileName; //ex: images/b1.gif
                    }

                    //add new customer
                    var result = services.addCustomer(customers);
                    if (!result)
                    {
                        ViewBag.error = "Email or Username is exist!";
                        return View();
                    }

                    //add trial package for customer
                    var payment = paymentPackageService.AddTrialPaymentPackageForCustomer(customers.Id);
                    if (payment == null)
                    {
                        Message = "Add Trial Package Fail! Please contact admin to help!";
                        return RedirectToAction("Login");
                    }

                    //send verification email
                    DateTime created_at = payment.Created_at;
                    string code = created_at.ToString("yyyyMMddHHmmssffff") + "-" + payment.Id;
                    string confirmUrl = Url.Action("Confirm", "Auth", new { code = code}, Request.Scheme);
                    bool processEmail = emailService.SendEmailRegisterAccount(confirmUrl, customers.Email);
                    if (!processEmail)
                    {
                        Message = "Send email fail! Please contact admin to help!";
                        return RedirectToAction("Login");
                    }

                    Message = "Register Successfull! Please check email to complete register!";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("customer");
            return RedirectToAction("Login");
        }

        public IActionResult Confirm(string code)
        {
            try
            {
                //check code
                var payment = paymentPackageService.CheckPaymentCode(code);
                if (payment == null)
                {
                    Message = "Invalid code";
                    return RedirectToAction("Login");
                }

                //update status for customer
                var result = services.updateStatus(payment.Customer_id, true, true);
                if (!result)
                {
                    Message = "Customer not found";
                    return RedirectToAction("Login");
                }

                Message = "Confirm Email Successful!";
            }
            catch (Exception)
            {
                Message = "Invalid code";
            }
            return RedirectToAction("Login");
        }
    }
}
