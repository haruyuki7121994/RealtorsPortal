using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class StaticsController : Controller
    {
        private readonly Services.IStaticService services;
        public StaticsController(Services.IStaticService services)
        {
            this.services = services;
        }
        public IActionResult Index(int Id)
        {
            var result = services.FindAllHistory();
            var res = result.Count(e=>e.Id == Id);
            return View(res);
        }
    }
}
