using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Real_Estate_Listing.Models;
using Newtonsoft.Json;

namespace Real_Estate_Listing.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //var allProperties = db.RealEstateProperties.OrderBy(x => x.id).ToList();
            return RedirectToAction("responsive");
        }

        public ActionResult LoadProp()
        {
            var allProperties = db.RealEstateProperties.OrderBy(x => x.id).ToList();
            var jsonString = JsonConvert.SerializeObject(allProperties, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Content(jsonString);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult responsive()
        {
            ViewBag.Message = "Responsive page.";

            return View();
        }

        public ActionResult dynamic()
        {
            ViewBag.Message = "Dynamic page.";

            return View();
        }

        public ActionResult location()
        {
            ViewBag.Message = "Location";

            return View();
        }
    }
}