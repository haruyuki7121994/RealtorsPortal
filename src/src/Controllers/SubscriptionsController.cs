using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly Services.IPaymentPackageService services;
        public SubscriptionsController(Services.IPaymentPackageService services)
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
        public IActionResult Create(Models.PaymentPackage payment_Package)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.addPaymentPackage(payment_Package);
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

            Models.PaymentPackage payment_Package = services.fineOne(id);
            return View(payment_Package);
        }
        [HttpPost]
        public IActionResult edit(Models.PaymentPackage payment_Package)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    services.updatePaymentPackage(payment_Package);
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
                services.deletePaymentPackage(id);
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
