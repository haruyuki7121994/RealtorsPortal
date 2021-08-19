using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriptionsController : Controller
    {
        private readonly Services.IPackageService services;
        public SubscriptionsController(Services.IPackageService services)
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
        public IActionResult Create(Models.Package package)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.addPackage(package);
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

            Models.Package package = services.findOne(id);
            return View(package);
        }
        [HttpPost]
        public IActionResult edit(Models.Package package)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    services.updatePackage(package);
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
                services.deletepackage(id);
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
