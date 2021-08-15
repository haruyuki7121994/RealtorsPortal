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
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await _cityService.GetCities());
        }

        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var city = await _cityService.GetCityById(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var c = await _cityService.CreateEditCity(id);
            if (c == false) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // POST: Cities/Delete/5
       
    }
}
