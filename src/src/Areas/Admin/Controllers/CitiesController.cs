using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using src.Models;
using src.Repository;
using src.Services;

namespace src.Controllers
{
    [Area("Admin")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IRegionService _regionService;

        public CitiesController(ICityService cityService, IRegionService regionService)
        {
            _cityService = cityService;
            _regionService = regionService; 
        }

       
        public async Task<IActionResult> Index()
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            return View(await _cityService.GetCities());
        }

        
        public async Task<IActionResult> Create()
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            ViewData["Regions"] = new SelectList(await _regionService.GetRegions(), "Id", "Name"); ;
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Is_active,Region_id")] City city)
        {
            if (ModelState.IsValid)
            {
                var c = await _cityService.CreateEditCity(city);
                if (c == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

      
        public async Task<IActionResult> Edit(int id)
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;

            var city = await _cityService.GetCityById(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["Regions"] = new SelectList(await _regionService.GetRegions(), "Id", "Name", city.Region_id);
            return View(city);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Is_active,Region_id")] City city)
        {
            if (ModelState.IsValid)
            {
                var c = await _cityService.CreateEditCity(city);
                if (c == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            var c = await _cityService.DeleteCity(id);
            
            if (c == false) return NotFound();

            return RedirectToAction(nameof(Index));
        }

      
       
    }
}
