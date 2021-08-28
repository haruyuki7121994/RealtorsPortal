using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Services;

namespace src.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ConfirmationPropertiesController : Controller
    {
        private readonly IPropertyService _propertyservice;
        private readonly IPaymentPackageService _paymentPackageService;
        private readonly IConfigurationService _configurationService;

        public ConfirmationPropertiesController(
            IPropertyService propertyservice,
            IPaymentPackageService paymentPackageService,
            IConfigurationService configurationService
        )
        {
            _propertyservice = propertyservice;
            _paymentPackageService = paymentPackageService;
            _configurationService = configurationService;
        }

        [TempData]
        public string Message { get; set; }
        public async Task<IActionResult> Index(int? page)
        {
            var properties = await _propertyservice.GetProperties();
            properties = properties.Where(p => p.Is_active.Equals(false));
            properties = PaginatedList<Property>.CreateAsnyc(properties, page ?? 1, 10);
            return View(properties);
        }

        public IActionResult Details(int id)
        {
            var property =  _propertyservice.FindOneWithRelation(id);
            return View(property);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string type, Property property)
        {
            try
            {
                var pro = await _propertyservice.GetPropertyById(property.Id);
                if (pro == null) return NotFound();
                if (type == "Approve")
                {
                    var result = _paymentPackageService.CheckCanCreateAds(pro.Customer_id, pro.Is_featured);
                    if (result)
                    {
                        var expireAdsConfig = await _configurationService.GetConfigurationByObj("Expiration of ads");
                        var expireDays = expireAdsConfig != null ? Int32.Parse(expireAdsConfig.Val) : 30;
                        pro.Ended_at = DateTime.Now.AddDays(expireDays);
                        pro.Is_active = true;
                        var updatedPro = await _propertyservice.CreateEditProperty(pro);
                        if (updatedPro == null) return NotFound();
                        _paymentPackageService.UpdateUsedAdsForCustomer(updatedPro.Customer_id, updatedPro.Is_featured);
                        Message = "Approve Successfull";
                    }
                    else
                    {
                        Message = "Can't confirm because the customer has no more limit ads!";
                    }
                }
                if (type == "Reject")
                {
                    var deleted = await _propertyservice.DeleteProperty(pro.Id);
                    if (deleted == false)
                    {
                        Message = "Cannot delete this property";
                    }
                    else
                    {
                        Message = "Delete this property successful";
                    }
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
