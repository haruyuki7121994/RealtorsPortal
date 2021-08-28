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
            var pp = services.fineOne(id);
            return View(pp);
        }

        public IActionResult Approve(int? page)
        {
            var result = services.findAll();
            result = result.Where(r => r.Status == PaymentPackage.PENDING_STATUS).ToList();
            result = PaginatedList<PaymentPackage>.CreateAsnyc(result.ToList(), page ?? 1, 10);
            return View(result);
        }

        public IActionResult ApproveDetails(int id)
        {
            var pp = services.fineOne(id);
            if (pp != null && pp.Status.Equals(PaymentPackage.PENDING_STATUS))
            {
                return View(pp);
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult SubmitApprove(string type, PaymentPackage paymentPackage)
        {
            try
            {
                var pp = services.fineOne(paymentPackage.Id);
                if (pp == null) return NotFound();
                if (type == "Approve")
                {
                    pp.Status = PaymentPackage.APPROVED_STATUS;
                    services.updatePaymentPackage(pp);
                    Message = "Approve Successfull";
                }
                if (type == "Reject")
                {
                    services.deletePaymentPackage(paymentPackage.Id);
                    Message = "Delete successful";
                }

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
