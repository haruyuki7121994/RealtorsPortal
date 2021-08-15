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
    public class AreasController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly ICityService _cityService;

        public AreasController(IAreaService areaService, ICityService cityService)
        {
            _areaService = areaService;
            _cityService = cityService;
    }

       
        public async Task<IActionResult> Index()
        {
            return View(await _areaService.GetAreas());
        }

       
        public async Task<IActionResult> Create()
        {
            ViewData["Cities"] = new SelectList(await _cityService.GetCityByActive(true), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Is_active,City_id")] Area area)
        {
            if (ModelState.IsValid)
            {
               Area areRepo = await  _areaService.CreateEditArea(area);
                if (areRepo == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = new SelectList(await _cityService.GetCityByActive(true), "Id", "Name", area.City_id);
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            Area area = await _areaService.GetAreaById(id);
            if (area == null)
            {
                return NotFound();
            }
            ViewData["Cities"] = new SelectList(await _cityService.GetCityByActive(true), "Id", "Name",area.City_id);
            return View(area);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Is_active,City_id")] Area area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Area areRepo = await _areaService.CreateEditArea(area);
                if (areRepo == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = new SelectList(await _cityService.GetCityByActive(true), "Id", "Name", area.City_id);
            return View(area);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _areaService.deleteArea(id);
            if (deleted == false) return NotFound();
            return RedirectToAction(nameof(Index));
        }

    }
}
