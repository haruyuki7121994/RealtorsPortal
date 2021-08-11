using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class SellersController : Controller
    {
        private readonly Services.ICustomersServices services;
        public SellersController(Services.ICustomersServices services)
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
        public IActionResult Create(Models.Customers customers, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file.Length > 0)
                    {
                        var filepath = Path.Combine("wwwroot/images", file.FileName);
                        var stream = new FileStream(filepath, FileMode.Create);
                        file.CopyToAsync(stream);
                        customers.Image = "images/" + file.FileName; //ex: images/b1.gif
                        services.addCustomer(customers);
                        return RedirectToAction("Index");
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

        [HttpGet]
        public IActionResult edit(int id)
        {
            Models.Customers customers = services.fineOne(id);
            return View(customers);
        }
        [HttpPost]
        public IActionResult editsellers(Models.Customers customers, IFormFile file)
        {
            try
            {
                Models.Customers cus = services.fineOne(customers.Id);
                if (ModelState.IsValid)
                {
                    if (file.Length > 0)
                    {
                        var path = Path.Combine("wwwroot/images", file.FileName);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        cus.Image = "images/" + file.FileName;
                        services.updateCustomer(customers);
                        return RedirectToAction("Index");
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
        public IActionResult Delete(int id)
        {

            try
            {
                services.deleteCustomer(id);
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
