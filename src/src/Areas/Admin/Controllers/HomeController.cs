using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var json = HttpContext.Session.GetString("user");
            if(json == null)
            {
                return RedirectToAction("Index", "Login");
            }    
            Models.Admin user = JsonConvert.DeserializeObject<Models.Admin>(json);
            ViewBag.Username = user.Username;


            return View();
        }
    }
}
