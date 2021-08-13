using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using src.Models;
using System.IO;

namespace src.Controllers
{
    public class AuthController : Controller
    {
        private readonly Services.ICustomerService services;
        public AuthController(Services.ICustomerService services)
        {
            this.services = services;
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
                var cus = services.checkLogin(username, password);
                if (cus != null)
                {
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
                    if (file != null)
                    {
                        var filepath = Path.Combine("wwwroot/images/avatars", file.FileName);
                        var stream = new FileStream(filepath, FileMode.Create);
                        file.CopyToAsync(stream);
                        customers.Image = "images/avatars" + file.FileName; //ex: images/b1.gif
                    }
                    var result = services.addCustomer(customers);
                    if (result)
                    {
                        Message = "Register Successful";
                        return RedirectToAction("Login");
                    }
                    
                    else
                    {
                        ViewBag.Msg = "Fail";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("customer", null);
            return RedirectToAction("Login");
        }
    }
}
