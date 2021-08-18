using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using src.Models;
using src.Services;

namespace src.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropertyService _propertyService;
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly IAreaService _areaService;
        private readonly IImageService _imageService;
        private readonly ICommentService _commentService;

        public HomeController
        (
            ILogger<HomeController> logger,
            IPropertyService propertyService,
            ICategoryService categoryService,
            ICountryService countryService,
            IRegionService regionService,
            ICityService cityService,
            IAreaService areaService,
            IImageService imageService,
            ICommentService commentService
        )
        {
            _logger = logger;
            this._propertyService = propertyService;
            this._categoryService = categoryService;
            this._countryService = countryService;
            this._regionService = regionService;
            this._cityService = cityService;
            this._areaService = areaService;
            this._imageService = imageService;
            this._commentService = commentService;
        }

        public async Task<IActionResult>Index()
        {
            var cagetories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            var properties = _propertyService.FindAllWithRelation();

            var featuredProps = properties.Where(p => p.Is_active.Equals(true) && p.Is_featured.Equals(true))
                .OrderByDescending(p => p.Created_at)
                .Take(9)
                .ToList();

            dynamic model = new ExpandoObject();
            model.featuredProps = featuredProps;
            model.categories = cagetories;
            return View(model);
        }

        public async Task<IActionResult> Properties
        (
            string method,
            string customer_role,
            int? category_id,
            string key,
            int? min_price,
            int? max_price,
            int? country_id,
            int? region_id,
            int? city_id,
            int? area_id,
            string sort_by,
            int limit,
            int? page
        )
        {
            var properties = _propertyService.FindAllWithRelation().Where(p => p.Is_active.Equals(true));

            var featuredProps = properties.Where(p => p.Is_featured.Equals(true))
                .OrderByDescending(p => p.Created_at)
                .Take(3)
                .ToList();

            var cagetories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            var countries = new SelectList(await _countryService.GetCountries(), "Id", "Name") ;

            //filer method
            ViewBag.method = method;
            if (!string.IsNullOrEmpty(method))
            {
                properties = properties.Where(p => p.Method.Equals(method));
            }

            //filter category
            ViewBag.category_id = category_id;
            if (category_id > 0)
            {
                properties = properties.Where(p => p.Category.Id.Equals(category_id));
            }

            //filter customer role
            ViewBag.customer_role = customer_role;
            if (!string.IsNullOrEmpty(customer_role))
            {
                properties = properties.Where(p => p.Customer.Type.Equals(customer_role));
            }

            //filter title
            ViewBag.key = key;
            if (!string.IsNullOrEmpty(key))
            {
                properties = properties.Where(p => p.Title.ToLower().Contains(key.ToLower()));
            }

            //filter min price
            ViewBag.min_price = min_price;
            if (min_price != null)
            {
                properties = properties.Where(p => p.Price >= min_price);
            }

            //filter max price
            ViewBag.max_price = max_price;
            if (max_price != null)
            {
                properties = properties.Where(p => p.Price <= max_price);
            }


            //filter location
            ViewBag.area_id = area_id;
            ViewBag.city_id = city_id;
            ViewBag.region_id = region_id;
            ViewBag.country_id = country_id;

            if (area_id > 0)
            {
                properties = properties.Where(p => p.Area.Id.Equals(area_id));
            }
            else if (city_id > 0)
            {
                properties = properties.Where(p => p.City.Id.Equals(city_id));
            }
            else if (region_id > 0)
            {
                properties = properties.Where(p => p.Region.Id.Equals(region_id));
            }
            else if (country_id > 0)
            {
                properties = properties.Where(p => p.Country.Id.Equals(country_id));
            }

            //filter sort by
            ViewBag.sort_by = sort_by;
            switch (sort_by)
            {
                case "name-desc":
                    properties = properties.OrderByDescending(p => p.Title);
                    break;
                case "name-asc":
                    properties = properties.OrderBy(p => p.Title);
                    break;
                case "price-desc":
                    properties = properties.OrderByDescending(p => p.Price);
                    break;
                case "price-asc":
                    properties = properties.OrderBy(p => p.Price);
                    break;
                case "date-asc":
                    properties = properties.OrderBy(p => p.Created_at);
                    break;
                default:
                    properties = properties.OrderByDescending(p => p.Created_at);
                    break;
            }

            
            int pageNumber = page ?? 1;
            int limit_per_page = limit > 0 ? limit : 6;
            ViewBag.page = pageNumber;
            ViewBag.limit = limit_per_page;


            //filter limit
            properties = PaginatedList<Property>.CreateAsnyc(properties.ToList(), pageNumber, limit_per_page);

            PropertiesPagingModel model = new PropertiesPagingModel
            {
                Categories = cagetories,
                Countries = countries,
                FeaturedProperties = featuredProps,
                PagingProperies = properties,
            };

            return View(model);
        }

        public async Task<IActionResult> Property(int id)
        {
            var prop = _propertyService.FindOneWithRelation(id);
            if (prop != null)
            {
                if (prop.Is_active.Equals(true))
                {
                    var properties = _propertyService.FindAllWithRelation().Where(p => p.Is_active.Equals(true));

                    var featuredProps = properties.Where(p => p.Is_featured.Equals(true))
                        .OrderByDescending(p => p.Created_at)
                        .Take(10)
                        .ToList();

                    var images = _imageService.FindByPropertyId(id);

                    var model = new PropertyPageModel
                    {
                        Property = prop,
                        FeaturedProps = featuredProps,
                        Gallary = images
                    };

                    ViewBag.Property_id = prop.Id;

                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Comment(Comment comment)
        {
            try
            {
                _commentService.addComment(comment);
                Message = "Send Message Successful!";
                return RedirectToAction("Property", new { id = comment.Property_id });
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }
        public IActionResult Blogs()
        {
            return View();
        }

        public IActionResult Blog1()
        {
            return View();
        }
        public IActionResult Blog2()
        {
            return View();
        }
     
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _countryService.GetCountries();
        }

        [TempData]
        public string Message { get; set; }
    }
}
