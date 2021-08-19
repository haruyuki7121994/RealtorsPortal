using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
    public class ConfirmationController : Controller
    {
        private readonly Services.ICustomerService _customerService;
        private readonly Services.IPaymentPackageService _paymentPackageService;
        private readonly Services.IEmailService _emailService;

        public ConfirmationController
        (
            Services.ICustomerService customerService,
            Services.IPaymentPackageService paymentPackageService,
            Services.IEmailService emailService
        )
        {
            this._customerService = customerService;
            this._paymentPackageService = paymentPackageService;
            this._emailService = emailService;
        }


        [TempData]
        public string Message { get; set; }

        public IActionResult Index()
        {
            var cus = _customerService.findAll();
            cus = cus.Where(c => c.Is_verified.Equals(false) && c.Is_active.Equals(false)).ToList();
            return View(cus);
        }

        public IActionResult Resend(string username)
        {
            var cus = _customerService.fineOne(username);
            var payments = _paymentPackageService.FindPackagesByCustomerId(cus.Id);
            var payment = payments.Where(p => p.Package_id == 1).SingleOrDefault();
            if (payment != null)
            {
                DateTime created_at = payment.Created_at;
                string code = created_at.ToString("yyyyMMddHHmmssffff") + "-" + payment.Id;
                string confirmUrl = Url.Action("Confirm", "Auth", new { code = code }, Request.Scheme);
                bool processEmail = _emailService.SendEmailRegisterAccount(confirmUrl, cus.Email);
                if (processEmail)
                {
                    Message = "Resend Email Successfull!";
                    return RedirectToAction("Index");
                }
            }
            Message = "Cannot resend email!";
            return RedirectToAction("Index");
        }
    }
}
