using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
    public class PaymenttransactionsController : Controller
    {
        private readonly Services.IPaymentPackageService services;
        public PaymenttransactionsController(Services.IPaymentPackageService services)
        {
            this.services = services;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index(DateTime startdate,DateTime enddate, int? page)
        {
            var result = services.findAll();
            if (startdate != enddate)
            {
                result = result.Where(e => e.Created_at >= startdate && e.Created_at <= enddate).ToList();
            }
            result = PaginatedList<PaymentPackage>.CreateAsnyc(result.ToList(), page ?? 1, 10);
            return View(result);
        }

        public IActionResult Details(int id)
        {
            return View(services.fineOne(id));
        }
    }
}
