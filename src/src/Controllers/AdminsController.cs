using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class AdminsController : Controller
    {
        private readonly Services.IAdminsServices services;
        public AdminsController(Services.IAdminsServices services)
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
        public IActionResult createadmin(Models.Admins admins)
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
        public IActionResult edit(int id)
        {

            Models.Admins admins = services.fineOne(id);
            return View(admins);
        }
        [HttpPost]
        public IActionResult edit(Models.Admins admins)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    services.updateAdmin(admins);
                    return RedirectToAction("Index");
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
