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
        private readonly IConfigurationService configurationService;
        private readonly IPaymentSubscriptionService paymentSubscriptionService;
        public AuthController
        (
            ICustomerService services, 
            IEmailService emailService, 
            IPaymentPackageService paymentPackageService,
            IConfigurationService configurationService,
            IPaymentSubscriptionService paymentSubscriptionService
        )
        {
            this.services = services;
            this.emailService = emailService;
            this.paymentPackageService = paymentPackageService;
            this.configurationService = configurationService;
            this.paymentSubscriptionService = paymentSubscriptionService;
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

                    //check enable payment subscription
                    cus.isPaymentSubscription = services.checkPaymentSubscription(cus);

                    //store session
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

        public async Task<IActionResult> PaymentSubscription()
        {
            //check session customer
            if (GetCustomerFromSession() == null)
            {
                return NotFound();
            }

            var enabledConfig = await configurationService.GetConfigurationByObj("Payment Subscription");
            var enabled = enabledConfig != null ? enabledConfig.Val.ToLower().Equals("enabled") : false;
            if (enabled)
            {
                //get price
                var priceConfig = await configurationService.GetConfigurationByObj("Subscription price");
                var price = priceConfig != null ? Int32.Parse(priceConfig.Val) : 0;
                if (price <= 0)
                {
                    return NotFound();
                }
                ViewBag.price = price;

                //get paypal key
                var paypalConfig = await configurationService.GetConfigurationByObj("Paypal key");
                ViewBag.paypalKey = paypalConfig != null ? paypalConfig.Val : "abc";
                return View();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult SuccessPayment(PaymentSubscription paymentSubscription)
        {
            //check session customer
            var cus = GetCustomerFromSession();
            if (cus == null)
            {
                return NotFound();
            }

            //add payment subscription
            DateTime now = DateTime.Now;
            paymentSubscription.Customer_id = cus.Id;
            paymentSubscription.Created_at = now;
            paymentSubscription.Updated_at = now;
            paymentSubscription.Transaction_id = $"{paymentSubscription.Type}{now:yyyyMMddHHmmssfff}";
            bool result = paymentSubscriptionService.addPaymentSubscription(paymentSubscription);
            if (!result)
            {
                Message = "Cannot Payment Subscription! Please contact admin for help!";
                return RedirectToAction("Login");
            }

            //send email
            paymentSubscription.Customer = cus;
            emailService.SendEmailPaymentSubscription(paymentSubscription);

            cus.isPaymentSubscription = result;
            HttpContext.Session.SetString("customer", JsonConvert.SerializeObject(cus));
            Message = "Payment Subscription Successfull! Please check email confirmation! We thank you for your support!";

            return RedirectToAction("Index", "Customer");
        }

        private Customer GetCustomerFromSession()
        {
            var session = HttpContext.Session.GetString("customer");
            if (session != null)
            {
                return JsonConvert.DeserializeObject<Customer>(session);
            }
            return null;
        }
    }
}
