using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminsController : Controller
    {
        private readonly Services.IAdminService services;
        public AdminsController(Services.IAdminService services)
        {
            this.services = services;
        }
        public IActionResult Index()
        {
            return View(services.findAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
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
    }
}