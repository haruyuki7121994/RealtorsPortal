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
using src.Services;

namespace src.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IPropertyService _propertyService;
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly IAreaService _areaService;
        private readonly IPaymentPackageService _paymentPackageService;
        private readonly IPackageService _packageService;
        private readonly IImageService _imageService;
        private readonly ICommentService _commentService;
        private readonly IConfigurationService _configurationService;
        public CustomerController
        (
            ICustomerService customerService,
            IPropertyService propertyService,
            ICategoryService categoryService,
            ICountryService countryService,
            IRegionService regionService,
            ICityService cityService,
            IAreaService areaService,
            IPaymentPackageService paymentPackageService,
            IPackageService packageService,
            IImageService imageService,
            ICommentService commentService,
            IConfigurationService configurationService
        )
        {
            this._customerService = customerService;
            this._propertyService = propertyService;
            this._categoryService = categoryService;
            this._countryService = countryService;
            this._regionService = regionService;
            this._cityService = cityService;
            this._areaService = areaService;
            this._paymentPackageService = paymentPackageService;
            this._packageService = packageService;
            this._imageService = imageService;
            this._commentService = commentService;
            this._configurationService = configurationService;
        }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index()
        {
            var cus = GetCustomerFromSession();
            dynamic model = new ExpandoObject();
            model.Properties = _propertyService.FindPropertiesByCustomerId(cus.Id).OrderByDescending(pp => pp.Created_at);
            model.PaymentPackages = _paymentPackageService.FindPackagesByCustomerId(cus.Id).OrderByDescending(pp => pp.Updated_at);
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
                    var result = _customerService.updateCustomer(customer);
                    if (result)
                    {
                        Message = "Update Successful";
                        cus = _customerService.checkLogin(customer.Username, customer.Password);
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

        public async Task<IActionResult> Create()
        {
            //check can or not create new ads
            var cus = GetCustomerFromSession();
            bool canCreateAds = _paymentPackageService.CheckCanCreateAds(cus.Id, false);

            if (canCreateAds)
            {
                //get dependencies
                ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
                ViewBag.Countries = new SelectList(await _countryService.GetCountries(), "Id", "Name");
                return View();
            }
            else
            {
                Message = "Cannot create ads! Please purchase new package!";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> CreateFeatured()
        {
            //check can or not create new ads
            var cus = GetCustomerFromSession();
            bool canCreateAds = _paymentPackageService.CheckCanCreateAds(cus.Id, true);

            if (canCreateAds)
            {
                //get dependencies
                ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
                ViewBag.Countries = new SelectList(await _countryService.GetCountries(), "Id", "Name");
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
        public async Task<IActionResult> CreateAds(Property property, IFormFile? file)
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
                            int scaleWidth = 370;
                            int scaleHeight = 220;
                            image.Mutate(x => x.Resize(scaleWidth, scaleHeight));
                            var newfileNameScale = GenerateFileName("scale_", systemFileExtenstion);
                            var filepath160 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "properties")).Root + $@"\{newfileNameScale}";
                            image.Save(filepath160);
                            property.Thumbnail_url = "images/properties/" + newfileNameScale; //ex: images/b1.gif
                        }
                    }

                    var cus = GetCustomerFromSession();
                    property.Customer_id = cus.Id;
                    var result = _propertyService.addProperty(property);
                    if (result)
                    {
                        Message = "Add Property Successful";
                        return RedirectToAction("Index");
                    }

                    ViewBag.error = "Fail";
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _countryService.GetCountries(), "Id", "Name");
            return View("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var property = _propertyService.FindOneWithRelation(id);
            var cus = GetCustomerFromSession();
            if (property.Customer_id == cus.Id)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
                ViewBag.Countries = new SelectList(await _countryService.GetCountries(), "Id", "Name");
                return View(property);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Property property, IFormFile? file)
        {
            var oldProp = _propertyService.FindOneWithRelation(property.Id);
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
                            int scaleWidth = 370;
                            int scaleHeight = 220;
                            image.Mutate(x => x.Resize(scaleWidth, scaleHeight));
                            var newfileNameScale = GenerateFileName("scale_", systemFileExtenstion);
                            var filepath160 = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "properties")).Root + $@"\{newfileNameScale}";
                            image.Save(filepath160);
                            property.Thumbnail_url = "images/properties/" + newfileNameScale; //ex: images/b1.gif
                        }
                    }

                    property.Is_active = oldProp.Is_active;
                    property.Customer_id = oldProp.Customer_id;
                    var result = _propertyService.updateProperty(property);
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
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _countryService.GetCountries(), "Id", "Name");
            return View(property);
        }

        public string GenerateFileName(string fileTypeName, string fileextenstion)
        {
            if (fileTypeName == null) throw new ArgumentNullException(nameof(fileTypeName));
            if (fileextenstion == null) throw new ArgumentNullException(nameof(fileextenstion));
            return $"{fileTypeName}_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}{fileextenstion}";
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteSuccess = await _propertyService.DeleteProperty(id);
            return RedirectToAction("Index");

        }

        public IActionResult Package()
        {
            var packages = _packageService.findAll()
                .Where(p => p.Is_active.Equals(true) && p.Name != "Trial")
                .ToList();
            return View(packages);
        }

        public async Task<IActionResult> Payment(string name)
        {
            var package = _packageService.fineOne(name);
            if (package != null)
            {
                var paypalKey = await _configurationService.GetConfigurationByObj("Paypal key");
                ViewBag.paypalKey = paypalKey.Val;
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
                _paymentPackageService.addPaymentPackage(newPayment);
                Message = "Payment Successful!";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Gallary(int id)
        {
            var cus = GetCustomerFromSession();
            var prop = await _propertyService.GetPropertyById(id);
            if (cus.Id == prop.Customer_id)
            {
                ViewBag.prop = prop;
                ViewBag.images = _imageService.FindByPropertyId(id);
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
                
                _imageService.addImage(image);
                Message = "Upload Successful";
                return RedirectToAction("Gallary", new { id = image.Property_id });
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            Message = "Cannot Upload Image";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteImage(Models.Image image)
        {
            try
            {
                _imageService.deleteImage(image.Id);
                Message = "Delete Successful";
                return RedirectToAction("Gallary", new { id = image.Property_id });
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View();
        }

        public IActionResult Comments(int id)
        {
            var comments = _commentService.FindByPropId(id);
            if (comments.Count() > 0)
            {
                return View(comments);
            }
            else
            {
                return RedirectToAction("Index");
            }
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

        public void SetCustomerFromSession(Customer customer)
        {
            HttpContext.Session.SetString("customer", JsonConvert.SerializeObject(customer));
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _countryService.GetCountries();
        }

        public async Task<ActionResult> GetRegions(int id)
        {
           
            ViewBag.Regions = new SelectList(await _regionService.GetRegionsByCountryId(id), "Id", "Name");
            return PartialView("DisplayRegions");
        }

        public async Task<ActionResult> GetCities(int id)
        {
            ViewBag.Cities = new SelectList( await _cityService.GetCitiesByRegionId(id), "Id", "Name");
            return PartialView("DisplayCities");
        }

        public async Task<ActionResult> GetAreas(int id)
        {
            ViewBag.Areas = new SelectList(await _areaService.GetAreasByCityId(id), "Id", "Name");
            return PartialView("DisplayAreas");
        }
    }
}
