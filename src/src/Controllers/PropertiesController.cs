using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Repository;
using src.Services;

namespace src.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyService _propertyservice;
        private readonly IAreaService _areaService;
        private readonly ICustomerService _customerService;
        private readonly ICategoryService _categoryService;

        public PropertiesController(IPropertyService propertyservice,
                                    IAreaService areaService,
                                    ICustomerService customerService,
                                    ICategoryService categoryService
                                    )
        {
            _propertyservice =  propertyservice;
            _areaService = areaService;
            _customerService = customerService;
            _categoryService = categoryService;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            return View(await _propertyservice.GetProperties());
        }

       
        public async Task<IActionResult> Create()
        {
            ViewData["Areas"] = new SelectList(await _areaService.GetAreaByActive(true), "Id", "Name");
            ViewData["Categories"] = new SelectList( _categoryService.findAll(), "Id", "Name");
            ViewData["Customers"] = new SelectList( _customerService.findAll(), "Id", "Name");
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Introduction,Description,Features,Method,Price,Deposit,Is_featured,Ended_at,Area_id,Category_id,Customer_id")] Property property)
        {
            if (ModelState.IsValid)
            {
                var pro = await _propertyservice.CreateEditProperty(property);
                if (pro == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(@property);
        }

      
        public async Task<IActionResult> Edit(int id)
        {

            var property = await _propertyservice.GetPropertyById(id);
            if(property == null)
            {
                return NotFound();
            }
            ViewData["Areas"] = new SelectList(await _areaService.GetAreaByActive(true), "Id", "Name", property.Area_id);
            ViewData["Categories"] = new SelectList(_categoryService.findAll(), "Id", "Name", property.Category_id);
            ViewData["Customers"] = new SelectList(_customerService.findAll(), "Id", "Name", property.Customer_id);
            return View(property);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Introduction,Description,Features,Method,Price,Deposit,Is_featured,Ended_at,Area_id,Category_id,Customer_id")] Property @property)
        {
           

            if (ModelState.IsValid)
            {
                var pro = await _propertyservice.CreateEditProperty(property);
                if (pro == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(@property);
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _propertyservice.DeleteProperty(id);
            if (deleted == false) return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
