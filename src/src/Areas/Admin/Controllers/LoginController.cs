using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using src.Models;
using src.Repository;
using src.Services;

namespace src.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IAdminService _adminService;

        public LoginController(IAdminService adminService)
        {
            _adminService = adminService;
        }

       
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public  IActionResult Index(Models.Admin admin)
        {
            Models.Admin adminRepo =  _adminService.checkLogin(admin.Username, admin.Password);
            if (adminRepo == null)
            {
                ViewBag.messLogin = "Username or Password is incorrect";
                return View();
            }
            else
            {
                if(adminRepo.Is_active==false)
                {
                    ViewBag.messLogin = "Account not active";
                    return View();
                }
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(adminRepo));

                return RedirectToAction("Index","Home");
            }    
                
           
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}
