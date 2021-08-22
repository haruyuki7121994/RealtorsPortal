using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationsController : Controller
    {
        private readonly Services.IPaymentPackageService services;
        private readonly Services.IEmailService emailService;
        private readonly Services.IPackageService packageService;
        public NotificationsController
        (
            Services.IPaymentPackageService services, 
            Services.IEmailService emailService,
            Services.IPackageService packageService
        )
        {
            this.services = services;
            this.emailService = emailService;
            this.packageService = packageService;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index(int? page)
        {
            var notify = services.FindByStatus(Models.PaymentPackage.EXPIRED_STATUS);
            notify = PaginatedList<PaymentPackage>.CreateAsnyc(notify.ToList(), page ?? 1, 10);
            return View(notify);
        }

        public IActionResult Send(int id)
        {
            var paymentPackage = services.fineOne(id);
            if (paymentPackage != null)
            {
                var result = emailService.SendEmailExpiredNotification(paymentPackage.Package, paymentPackage.Customer.Email);
                if (result)
                {
                    services.deletePaymentPackage(id);
                    ViewBag.Msg = "Send email successful!";
                    
                }
            }
            ViewBag.Msg = "Cannot send email!";
            return RedirectToAction("Index");
        }
    }
}
