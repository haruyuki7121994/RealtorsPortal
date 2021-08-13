using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class AgentsController : Controller
    {
        private readonly Services.ICustomerService services;
        public AgentsController(Services.ICustomerService services)
        {
            this.services = services;
        }
        public IActionResult Index()
        {
            var result = services.findAll();
            var res = result.Where(e => e.Type == "agent");
                return View(res);
          
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult Create(Models.Customer customers, IFormFile file)
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
        public IActionResult edit(string username)
        {
            Models.Customer customers = services.fineOne(username);
            return View(customers);
        }
        [HttpPost]
        public IActionResult Edit(Models.Customer customers, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        var path = Path.Combine("wwwroot/images/avatars", file.FileName);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        customers.Image = "images/avatars/" + file.FileName;
                        
                    }
                    services.updateCustomer(customers);
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
