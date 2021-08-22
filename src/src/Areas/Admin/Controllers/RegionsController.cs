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
    public class RegionsController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public RegionsController(IRegionService regionService, ICountryService countryService, ICityService cityService)
        {
            _regionService = regionService;
            _countryService = countryService;
            _cityService = cityService;
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
            var regions = await _regionService.GetRegions();
            regions = PaginatedList<Region>.CreateAsnyc(regions.ToList(), page ?? 1, 10);
            return View(regions);
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
            var region = await _regionService.GetRegionById(id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
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
            IEnumerable<Country> cate = await _countryService.GetCountries();
            SelectList cateList = new SelectList(cate,"Id","Name");

            ViewBag.Countries = cateList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Is_active,Country_id")] Region region)
        {
            if (ModelState.IsValid)
            {
                var r = await _regionService.CreateEditRegion(region);
                if (r == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country_id"] = new SelectList(await _countryService.GetCountries(), "Id", "Id", region.Country_id);
            return View(region);
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
            var region = await _regionService.GetRegionById(id);
            if (region == null) return NotFound();
            ViewData["Country_id"] = new SelectList(await _countryService.GetCountries(), "Id", "Name", region.Country_id);
            return View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Is_active,Country_id")] Region region)
        {
           

            if (ModelState.IsValid)
            {
                var r = await _regionService.CreateEditRegion(region);
                if (r == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country_id"] = new SelectList(await _countryService.GetCountries(), "Id", "Id", region.Country_id);
            return View(region);
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
            var cities = await _cityService.GetCitiesByRegionId(id);
            if (cities.Count() > 0)
            {
                Message = "Cannot delete this city!";
                return RedirectToAction("Index");
            }
            var region = await _regionService.DeleteRegion(id);
            if (region == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

     
    }
}
