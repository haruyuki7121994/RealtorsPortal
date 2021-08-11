using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class PaymenttransactionsController : Controller
    {
        private readonly Services.IPaymentPackageService services;
        public PaymenttransactionsController(Services.IPaymentPackageService services)
        {
            this.services = services;
        }
        public IActionResult Index()
        {
            return View(services.findAll());
        }
    }
}
