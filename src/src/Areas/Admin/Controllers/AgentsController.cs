using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
    public class AgentsController : Controller
    {
        private readonly Services.ICustomerService services;
        private readonly Services.IPropertyService _propertyServices;
        public AgentsController(Services.ICustomerService services, Services.IPropertyService propertyServices)
        {
            this.services = services;
            this._propertyServices = propertyServices;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index(int? page)
        {
            var result = services.findAll();
            var res = result.Where(e => e.Type == "agent");
            res = PaginatedList<Customer>.CreateAsnyc(res.ToList(), page ?? 1, 10);
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
                    if (file != null)
                    {
                        var filepath = Path.Combine("wwwroot/images", file.FileName);
                        var stream = new FileStream(filepath, FileMode.Create);
                        file.CopyToAsync(stream);
                        customers.Image = "images/" + file.FileName;
                    }

                    var result = services.addCustomer(customers);
                    if (!result)
                    {
                        ViewBag.error = "Email or Username is exist!";
                        return View();
                    }
                    else
                    {
                        Message = "Add Successfull";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
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
                    var result = services.updateCustomer(customers);
                    if (!result)
                    {
                        ViewBag.error = "Cannot update customer";
                        return View();
                    }
                    else
                    {
                        Message = "Edit Successfull";
                        return RedirectToAction("Index");
                    }
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
                var properties = await _propertyServices.GetPropertiesByCustomerId(id);
                if (properties.Count() > 0)
                {
                    Message = "Cannot delete customer!";
                    return RedirectToAction("Index");
                }
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
