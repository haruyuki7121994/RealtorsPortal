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
    public class ConfigurationsController : Controller
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationsController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        // GET: Configurations
        public async Task<IActionResult> Index()
        {
            return View(await _configurationService.GetConfigurations());
        }

       
       

       
        public async Task<IActionResult> Edit(string id)
        {


            var configuration = await _configurationService.GetConfigurationByObj(id);
            if (configuration == null)
            {
                return NotFound();
            }
            return View(configuration);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Obj,Val")] Configuration configuration)
        {
            

            if (ModelState.IsValid)
            {
                var config = await _configurationService.UpdateCongiguration(configuration);
                if (config == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(configuration);
        }

      
    }
}
