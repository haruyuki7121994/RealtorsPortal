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
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
           
            _categoryService =  categoryService;
        }

      
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetCategories());
        }

        public IActionResult Create()
        {
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
            var categoryRepo = await _categoryService.DeleteCategory(id);
            if (categoryRepo == false) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
