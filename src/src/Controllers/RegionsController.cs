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
    public class RegionsController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly ICountryService _countryService;

        public RegionsController(IRegionService regionService, ICountryService countryService)
        {
            _regionService = regionService;
            _countryService = countryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _regionService.GetRegions());
        }

        public async Task<IActionResult> Details(int id)
        {
          
            var region = await _regionService.GetRegionById(id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }
        public async Task<IActionResult> Create()
        {

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


            var region = await _regionService.DeleteRegion(id);
            if (region == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

     
    }
}
