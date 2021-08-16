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
        public IActionResult Index(DateTime startdate,DateTime enddate)
        {
            var result = services.findAll();
            if (startdate == enddate)
            {
                return View(result);
            }
            else
            {
                var res = result.Where(e => e.Created_at >= startdate && e.Created_at <= enddate);
                return View(res);
            }
        }
    }
}
