﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace src.Area.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly Services.ICategoryService categoryService;
        private readonly Services.ICustomerService customerService;
        private readonly Services.IPropertyService propertyService;
        private readonly Services.IPaymentPackageService paymentPackageService;
        public ReportsController
        (
            Services.ICategoryService categoryService,
            Services.ICustomerService customerService,
            Services.IPropertyService propertyService,
            Services.IPaymentPackageService paymentPackageService
        )
        {
            this.categoryService = categoryService;
            this.customerService = customerService;
            this.propertyService = propertyService;
            this.paymentPackageService = paymentPackageService;
        }
        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 0, 0, 1);
            DateTime endDate = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

            var properties = propertyService.FindAllWithRelation();
            var customers = customerService.findAll();
            var payments = paymentPackageService.findAll().Where(pp => pp.Created_at >= startDate && pp.Created_at <= endDate); ;

            var activeProperties = properties.Where(p => p.Is_active.Equals(true)).Count();
            var inactiveProperties = properties.Where(p => p.Is_active.Equals(false)).Count();
            var countCategories = (await categoryService.GetCategories()).Count();

            var countSellers = customers.Where(c => c.Type.Equals("private")).Count();
            var countAgents = customers.Where(c => c.Type.Equals("agent")).Count();
            var sumPayments = payments.Sum(p => p.Payment_price);

            ViewBag.countPayments = sumPayments;
            ViewBag.activeProperties = activeProperties;
            ViewBag.inactiveProperties = inactiveProperties;
            ViewBag.countCategories = countCategories;
            ViewBag.countSellers = countSellers;
            ViewBag.countAgents = countAgents;
            
            return View();
        }
    }
}