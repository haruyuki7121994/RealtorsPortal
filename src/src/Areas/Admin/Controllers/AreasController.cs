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

namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
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
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;
            return View(await _areaService.GetAreas());
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
            ViewData["Cities"] = new SelectList(await _cityService.GetCities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Is_active,City_id")] Models.Area are)
        {
            if (ModelState.IsValid)
            {
                Models.Area areRepo = await  _areaService.CreateEditArea(are);
                if (areRepo == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = new SelectList(await _cityService.GetCities(), "Id", "Name", are.City_id);
            return View(are);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;

            Models.Area area = await _areaService.GetAreaById(id);
            if (area == null)
            {
                return NotFound();
            }
            ViewData["Cities"] = new SelectList(await _cityService.GetCities(), "Id", "Name",area.City_id);
            return View(area);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City_id")] Models.Area are)
        {
            if (id != are.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Models.Area areRepo = await _areaService.CreateEditArea(are);
                if (areRepo == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = new SelectList(await _cityService.GetCities(), "Id", "Name", are.City_id);
            return View(are);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _areaService.deleteArea(id);
            if (deleted == false) return NotFound();
            return RedirectToAction(nameof(Index));
        }

    }
}
