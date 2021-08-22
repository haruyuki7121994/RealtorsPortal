using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriptionsController : Controller
    {
        private readonly Services.IPackageService services;
        private readonly Services.IPaymentPackageService _paymentPackageService;
        public SubscriptionsController(Services.IPackageService services, Services.IPaymentPackageService paymentPackageService)
        {
            this.services = services;
            this._paymentPackageService = paymentPackageService;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index(int? page)
        {
            var package = services.findAll().Where(p => p.Name != "Trial").ToList();
            package = PaginatedList<Package>.CreateAsnyc(package.ToList(), page ?? 1, 10);
            return View(package);
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


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var payments = await _paymentPackageService.GetPaymentsByPackageId(id);
                if (payments.Count() > 0)
                {
                    Message = "Cannot delete this package";
                    return RedirectToAction("Index");
                }
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
