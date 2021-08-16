﻿using System;
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
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _countryService.GetCountries());
        }
        public async Task<IActionResult> Details(int id)
        {
            var country = await _countryService.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Is_active")] Country country)
        {
            if (ModelState.IsValid)
            {
                var c = await _countryService.CreateEditCountry(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }  
        public async Task<IActionResult> Edit(int id)
        {
            var country = await _countryService.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Is_active")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var c = await _countryService.CreateEditCountry(country);
                if(c == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }
       public async Task<IActionResult> Delete(int id)
        {
            var deleteSuccess = await _countryService.DeleteCountry(id);
            if (!deleteSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
