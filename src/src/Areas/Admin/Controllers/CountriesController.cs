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
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IRegionService _regionService;

        public CountriesController(ICountryService countryService, IRegionService regionService)
        {
            _countryService = countryService;
            _regionService = regionService;
        }

        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> Index(int? page)
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            var countries = await _countryService.GetCountries();
            countries = PaginatedList<Country>.CreateAsnyc(countries.ToList(), page ?? 1, 10);
            return View(countries);
        }
        public async Task<IActionResult> Details(int id)
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            var country = await _countryService.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
        public IActionResult Create()
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
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
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
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
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            var regions = await _regionService.GetRegionsByCountryId(id);
            if (regions.Count() > 0)
            {
                Message = "Cannot delete this country!";
                return RedirectToAction(nameof(Index));
            }
            var deleteSuccess = await _countryService.DeleteCountry(id);
            if (!deleteSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
