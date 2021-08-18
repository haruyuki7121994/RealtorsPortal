﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class StaticsController : Controller
    {
        private readonly Services.ICategoryService categoryService;
        private readonly Services.ICustomerService customerService;
        private readonly Services.IPropertyService propertyService;
        private readonly Services.IPaymentPackageService paymentPackageService;
        public StaticsController
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
        public IActionResult Index()
        {
            var properties = customerService.findAll();
            var customers = customerService.findAll();

            var activeProperties = properties.Where(p => p.Is_active.Equals(true)).Count();
            var inactiveProperties = properties.Where(p => p.Is_active.Equals(false)).Count();
            var countCategories = categoryService.findAll().Count();
            var countSellers = customers.Where(c => c.Type.Equals("private")).Count();
            var countAgents = customers.Where(c => c.Type.Equals("agent")).Count();

            ViewBag.activeProperties = activeProperties;
            ViewBag.inactiveProperties = inactiveProperties;
            ViewBag.countCategories = countCategories;
            ViewBag.countSellers = countSellers;
            ViewBag.countAgents = countAgents;
            return View();
        }
    }
}
