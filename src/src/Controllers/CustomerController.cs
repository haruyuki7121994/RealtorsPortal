using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.Extensions.FileProviders;

namespace src.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Services.ICustomerService customerService;
        private readonly Services.IPropertyService propertyService;
        private readonly Services.ICategoryService categoryService;
        private readonly Services.ICountryService countryService;
        private readonly Services.IRegionservice regionService;
        private readonly Services.ICityService cityService;
        private readonly Services.IAreaService areaService;
        private readonly Services.IPaymentPackageService paymentPackageService;
        private readonly Services.IPackageService packageService;
        private readonly Services.IImageService imageService;
        public CustomerController
        (
            Services.ICustomerService customerService,
            Services.IPropertyService propertyService,
            Services.ICategoryService categoryService,
            Services.ICountryService countryService,
            Services.IRegionservice regionService,
            Services.ICityService cityService,
            Services.IAreaService areaService,
            Services.IPaymentPackageService paymentPackageService,
            Services.IPackageService packageService,
            Services.IImageService imageService
        )
        {
            this.customerService = customerService;
            this.propertyService = propertyService;
            this.categoryService = categoryService;
            this.countryService = countryService;
            this.regionService = regionService;
            this.cityService = cityService;
            this.areaService = areaService;
            this.paymentPackageService = paymentPackageService;
            this.packageService = packageService;
            this.imageService = imageService;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index()
        {
            var cus = GetCustomerFromSession();
            dynamic model = new ExpandoObject();
            model.Properties = propertyService.FindByCustomerId(cus.Id).OrderByDescending(p => p.Created_at);
            model.PaymentPackages = paymentPackageService.FindPackagesByCustomerId(cus.Id).OrderByDescending(pp => pp.Updated_at);
            model.Customer = cus;
            return View(model);
        }

        public IActionResult Profile()
        {
            var cus = GetCustomerFromSession();
            return View(cus);
        }

        [HttpPost]
        public IActionResult Profile(Customer customer, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        var filepath = Path.Combine("wwwroot/images/avatars", file.FileName);
                        var stream = new FileStream(filepath, FileMode.Create);
                        file.CopyToAsync(stream);
                        customer.Image = "images/avatars/" + file.FileName; //ex: images/b1.gif
                    }
                    var cus = GetCustomerFromSession();
                    customer.Is_active = cus.Is_active;
                    customer.Is_verified = cus.Is_verified;
                    var result = customerService.updateCustomer(customer);
                    if (result)
                    {
                        Message = "Update Successful";
                        cus = customerService.checkLogin(customer.Username, customer.Password);
                        HttpContext.Session.SetString("customer", JsonConvert.SerializeObject(cus));
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ViewBag.error = "Fail";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View(GetCustomerFromSession());
        }

        public IActionResult Create()
        {
            //check can or not create new ads
            var cus = GetCustomerFromSession();
            bool canCreateAds = paymentPackageService.CheckCanCreateAds(cus.Id, false);

            if (canCreateAds)
            {
                //get dependencies
                ViewBag.Categories = new SelectList(categoryService.findAll(true), "Id", "Name");
                ViewBag.Countries = new SelectList(GetCountries(), "Id", "Name");
                return View();
            }
            else
            {
                Message = "Cannot create ads! Please purchase new package!";
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateFeatured()
        {
            //check can or not create new ads
            var cus = GetCustomerFromSession();
            bool canCreateAds = paymentPackageService.CheckCanCreateAds(cus.Id, true);

            if (canCreateAds)
            {
                //get dependencies
                ViewBag.Categories = new SelectList(categoryService.findAll(true), "Id", "Name");
                ViewBag.Countries = new SelectList(GetCountries(), "Id", "Name");
                return View();
            }
            else
            {
                Message = "Cannot create ads! Please purchase new package!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAds(Property property, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = file.FileName;
                        using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                        {
                            string systemFileExtenstion = fileName.Substring(fileName.LastIndexOf('.'));
                            int scaleWidth = property.Is_featured ? 750 : 370;
                            int scaleHeight = property.Is_featured ? 500 : 220;
                            image.Mutate(x => x.Resize(scaleWidth, scaleHeight));
                            var newfileNameScale = GenerateFileName("scale_", systemFileExtenstion);
                            var filepath160 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "properties")).Root + $@"\{newfileNameScale}";
                            image.Save(filepath160);
                            property.Thumbnail_url = "images/properties/" + newfileNameScale; //ex: images/b1.gif
                        }
                    }

                    var cus = GetCustomerFromSession();
                    property.Customer_id = cus.Id;
                    var result = propertyService.addProperty(property);
                    if (result)
                    {
                        Message = "Add Property Successful";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Fail";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            ViewBag.Categories = new SelectList(categoryService.findAll(), "Id", "Name");
            ViewBag.Countries = new SelectList(GetCountries(), "Id", "Name");
            return View();
        }

        public IActionResult Edit(int id)
        {
            var property = propertyService.FindOneWithRelation(id);
            var cus = GetCustomerFromSession();
            if (property.Customer_id == cus.Id)
            {
                ViewBag.Categories = new SelectList(categoryService.findAll(true), "Id", "Name");
                ViewBag.Countries = new SelectList(GetCountries(), "Id", "Name");
                return View(property);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Property property, IFormFile? file)
        {
            var oldProp = propertyService.FindOneWithRelation(property.Id);
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = file.FileName;
                        using (var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                        {
                            string systemFileExtenstion = fileName.Substring(fileName.LastIndexOf('.'));
                            int scaleWidth = property.Is_featured ? 750 : 370;
                            int scaleHeight = property.Is_featured ? 500 : 220;
                            image.Mutate(x => x.Resize(scaleWidth, scaleHeight));
                            var newfileNameScale = GenerateFileName("scale_", systemFileExtenstion);
                            var filepath160 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "properties")).Root + $@"\{newfileNameScale}";
                            image.Save(filepath160);
                            property.Thumbnail_url = "images/properties/" + newfileNameScale; //ex: images/b1.gif
                        }
                    }

                    property.Is_active = oldProp.Is_active;
                    property.Customer_id = oldProp.Customer_id;
                    var result = propertyService.updateProperty(property);
                    if (result)
                    {
                        Message = "Update Property Successful";
                        return RedirectToAction("Edit", new { id = oldProp.Id});
                    }
                    else
                    {
                        ViewBag.error = "Cannot Update Property";
                    }
                }
                else
                {
                    ViewBag.Categories = new SelectList(categoryService.findAll(true), "Id", "Name");
                    ViewBag.Countries = new SelectList(GetCountries(), "Id", "Name");
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            
            return View(oldProp);
        }

        public string GenerateFileName(string fileTypeName, string fileextenstion)
        {
            if (fileTypeName == null) throw new ArgumentNullException(nameof(fileTypeName));
            if (fileextenstion == null) throw new ArgumentNullException(nameof(fileextenstion));
            return $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{fileextenstion}";
        }

        public IActionResult Delete(int id)
        {
            propertyService.deleteProperty(id);
            return RedirectToAction("Index");
        }

        public IActionResult Package()
        {
            var packages = packageService.findAll()
                .Where(p => p.Is_active.Equals(true) && p.Name != "Trial")
                .ToList();
            return View(packages);
        }

        public IActionResult Payment(string name)
        {
            var package = packageService.fineOne(name);
            if (package != null)
            {
                return View(package);
            }
            Message = "Package Not Found!";
            return RedirectToAction("Package");
        }

        public IActionResult Success(Package package)
        {
            try
            {
                var cus = GetCustomerFromSession();
                var now = DateTime.Now;
                var newPayment = new PaymentPackage
                {
                    Customer_id = cus.Id,
                    Package_id = package.Id,
                    Payment_price = package.Price,
                    Limit_ads = package.Limit_ads,
                    Limit_featured_ads = package.Limit_featured_ads,
                    Created_at = now,
                    Updated_at = now,
                    Transaction_id = $"{package.Name}{now:yyyyMMddHHmmssfff}",
                    Status = PaymentPackage.APPROVED_STATUS
                };
                paymentPackageService.addPaymentPackage(newPayment);
                Message = "Payment Successful!";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Gallary(int id)
        {
            var cus = GetCustomerFromSession();
            var prop = propertyService.FindOneWithRelation(id);
            if (cus.Id == prop.Customer_id)
            {
                ViewBag.prop = propertyService.FindOneWithRelation(id);
                ViewBag.images = imageService.FindByPropertyId(id);
                return View();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Upload(Models.Image image, IFormFile? file)
        {
            try
            {
                if (file != null)
                {
                    string fileName = file.FileName;
                    using (var processImage = SixLabors.ImageSharp.Image.Load(file.OpenReadStream()))
                    {
                        string systemFileExtenstion = fileName.Substring(fileName.LastIndexOf('.'));
                        int scaleWidth = 750;
                        int scaleHeight = 500;
                        processImage.Mutate(x => x.Resize(scaleWidth, scaleHeight));
                        var newfileNameScale = GenerateFileName("scale_", systemFileExtenstion);
                        var filepath160 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "properties")).Root + $@"\{newfileNameScale}";
                        processImage.Save(filepath160);
                        image.Url = "images/properties/" + newfileNameScale; //ex: images/b1.gif
                    }
                }
                
                imageService.addImage(image);
                Message = "Upload Successful";
                return RedirectToAction("Gallary", new { id = image.Property_id });
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteImage(Models.Image image)
        {
            try
            {
                imageService.deleteImage(image.Id);
                Message = "Delete Successful";
                return RedirectToAction("Gallary", new { id = image.Property_id });
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View();
        }

        public Customer GetCustomerFromSession()
        {
            if (HttpContext.Session.GetString("customer") != null)
            {
                var jsonCus = HttpContext.Session.GetString("customer");
                return JsonConvert.DeserializeObject<Customer>(jsonCus);
            }
            return null;
        }

        public List<Country> GetCountries()
        {
            return countryService.FindAll();
        }

        public ActionResult GetRegions(int id)
        {
            var regions = regionService.findAll();
            regions = regions.Where(r => r.Country_id.Equals(id)).ToList();
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            return PartialView("DisplayRegions");
        }

        public ActionResult GetCities(int id)
        {
            var cities = cityService.findAll();
            cities = cities.Where(c => c.Region_id.Equals(id)).ToList();
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
            return PartialView("DisplayCities");
        }

        public ActionResult GetAreas(int id)
        {
            var areas = areaService.findAll();
            areas = areas.Where(a => a.City_id.Equals(id)).ToList();
            ViewBag.Areas = new SelectList(areas, "Id", "Name");
            return PartialView("DisplayAreas");
        }
    }
}
