using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class ConfirmationsController : Controller
    {
        private readonly Services.ICustomersServices services;
        public ConfirmationsController(Services.ICustomersServices services)
        {
            this.services = services;
        }
        public IActionResult Index()
        {
            return View(services.findAll());
        }
    }
}
