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
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
           
            _categoryService =  categoryService;
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
            return View(await _categoryService.GetCategories());
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
        public async Task<IActionResult> Create([Bind("Id,Name,Is_active")] Category category)
        {
            if (ModelState.IsValid)
            {
              var categoryRepo =await  _categoryService.CreateEditCategory(category);
                if (categoryRepo == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var json = HttpContext.Session.GetString("user");
            if (json == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;

            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Is_active")] Category category)
        {
          

            if (ModelState.IsValid)
            {
                var categoryRepo = await _categoryService.CreateEditCategory(category);
                if (categoryRepo == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
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
            var categoryRepo = await _categoryService.DeleteCategory(id);
            if (categoryRepo == false) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
