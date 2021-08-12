using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class ConfirmationsController : Controller
    {
        private readonly Services.ICustomerService services;
        public ConfirmationsController(Services.ICustomerService services)
        {
            this.services = services;
        }
        public IActionResult Index()
        {
            var result = services.findAll();
            var res = result.Where(e => e.Is_active = false);
            return View(res);
        }
    }
}
