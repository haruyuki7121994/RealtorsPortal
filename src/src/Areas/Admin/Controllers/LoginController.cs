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

        [TempData]
        public string Message { get; set; }

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
                ViewBag.messLogin = "Username - Password is incorrect or Account is blocked";
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

                return RedirectToAction("Index","Reports");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult ChangePassword()
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangePassword(Models.Admin ad)
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            user.Password = ad.Password;
            _adminService.updateAdmin(user);
            Message = "Update Password Successfull!";
            return RedirectToAction("Index", "Reports");
        }
    }
}
