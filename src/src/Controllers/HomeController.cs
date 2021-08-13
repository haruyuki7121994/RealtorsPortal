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

namespace src.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Services.IPropertyService propertyService;
        private readonly Services.ICategoryService categoryService;
        private readonly Services.ICountryService countryService;
        private readonly Services.IRegionservice regionService;
        private readonly Services.ICityService cityService;
        private readonly Services.IAreaService areaService;

        public HomeController
        (
            ILogger<HomeController> logger,
            Services.IPropertyService propertyService,
            Services.ICategoryService categoryService,
            Services.ICountryService countryService,
            Services.IRegionservice regionService,
            Services.ICityService cityService,
            Services.IAreaService areaService
        )
        {
            _logger = logger;
            this.propertyService = propertyService;
            this.categoryService = categoryService;
            this.countryService = countryService;
            this.regionService = regionService;
            this.cityService = cityService;
            this.areaService = areaService;
        }

        public IActionResult Index()
        {
            var cagetories = new SelectList(categoryService.findAll(true), "Id", "Name");
            var properties = propertyService.FindAllWithRelation();
            var lastestProps = properties.Where(p => p.Is_active.Equals(true))
                .OrderByDescending(p => p.Created_at)
                .Take(6)
                .ToList();

            var featuredProps = properties.Where(p => p.Is_active.Equals(true) && p.Is_featured.Equals(true))
                .OrderByDescending(p => p.Created_at)
                .Take(6)
                .ToList();

            dynamic model = new ExpandoObject();
            model.lastestProps = lastestProps;
            model.featuredProps = featuredProps;
            model.categories = cagetories;
            return View(model);
        }

        public IActionResult Properties
        (
            string method,
            string customer_role,
            int? category_id,
            string title,
            int? min_price,
            int? max_price,
            int? country_id,
            int? region_id,
            int? city_id,
            int? area_id,
            string sort_by,
            int limit
        )
        {
            var properties = propertyService.FindAllWithRelation().Where(p => p.Is_active.Equals(true));
            var featuredProps = properties.Where(p => p.Is_featured.Equals(true))
                .OrderByDescending(p => p.Created_at)
                .Take(3)
                .ToList();

            var cagetories = new SelectList(categoryService.findAll(true), "Id", "Name");
            var countries = new SelectList(GetCountries(), "Id", "Name");

            int limit_per_page = limit > 0 ? limit : 6 ;

            //filer method
            if (!string.IsNullOrEmpty(method))
            {
                properties = properties.Where(p => p.Method.Equals(method));
            }

            //filter category
            if (category_id > 0)
            {
                properties = properties.Where(p => p.Category.Id.Equals(category_id));
            }

            //filter customer role
            if (!string.IsNullOrEmpty(customer_role))
            {
                properties = properties.Where(p => p.Customer.Type.Equals(customer_role));
            }

            //filter title
            if (!string.IsNullOrEmpty(title))
            {
                properties = properties.Where(p => p.Title.ToLower().Contains(title.ToLower()));
            }

            //filter min price
            if (min_price != null)
            {
                properties = properties.Where(p => p.Price >= min_price);
            }

            //filter max price
            if (max_price != null)
            {
                properties = properties.Where(p => p.Price <= max_price);
            }


            //filter location
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

            //filter limit
            properties = properties.Take(limit_per_page).ToList();

            dynamic model = new ExpandoObject();
            model.filtedProps = properties;
            model.categories = cagetories;
            model.countries = countries;
            model.featuredProps = featuredProps;

            return View(model);
        }

        public IActionResult Property(int id)
        {
            var prop = propertyService.FindOneWithRelation(id);
            if (prop != null)
            {
                var featuredProps = propertyService.FindAllWithRelation()
                .Where(p => p.Is_active.Equals(true) && p.Is_featured.Equals(true))
                .OrderByDescending(p => p.Created_at)
                .Take(10)
                .ToList();

                dynamic model = new ExpandoObject();
                model.currentProp = prop;
                model.featuredProps = featuredProps;

                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Blogs()
        {
            return View();
        }

        public IActionResult Blog()
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

        public List<Country> GetCountries()
        {
            return countryService.FindAll();
        }
    }
}
