using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using src.Models;

namespace src.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly Services.IAdminService services;
        public UsersController(Services.IAdminService services)
        {
            this.services = services;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index(int? page)
        {
            if (!CheckRoleUserFromSession())
            {
                Message = "You are not superadmin";
                return RedirectToAction("Index", "Reports");
            }
            var users = services.findAll().Where(a => a.Role != "superadmin").ToList();
            users = PaginatedList<Models.Admin>.CreateAsnyc(users.ToList(), page ?? 1, 10);
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (!CheckRoleUserFromSession())
            {
                Message = "You are not superadmin";
                return RedirectToAction("Index", "Reports");
            }
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult createadmin(Models.Admin admins)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.addAdmin(admins);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!CheckRoleUserFromSession())
            {
                Message = "You are not superadmin";
                return RedirectToAction("Index", "Reports");
            }
            Models.Admin admins = services.fineOne(id);
            return View(admins);
        }
        [HttpPost]
        public IActionResult Edit(Models.Admin admins)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (admins.Password == null)
                    {
                        var oldAdmin = services.fineOne(admins.Id);
                        if (oldAdmin != null)
                        {
                            admins.Password = oldAdmin.Password;
                        }
                    }
                    services.updateAdmin(admins);
                    Message = "Update Successfull";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "Fail";
                }

            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }


        public IActionResult Delete(int id)
        {
            try
            {
                services.deleteAdmin(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public bool CheckRoleUserFromSession()
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return false;
            }
            var user = JsonConvert.DeserializeObject<Models.Admin>(json);
            if (user.Role.Equals("superadmin"))
            {
                return true;
            }
            return false;
        }
    }
}