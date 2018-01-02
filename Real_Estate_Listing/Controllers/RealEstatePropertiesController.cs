using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Real_Estate_Listing.Models;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;

namespace Real_Estate_Listing.Controllers
{
    public class RealEstatePropertiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RealEstateProperties
        public ActionResult Index()
        {
            var realEstateProperties = db.RealEstateProperties.Include(r => r.Category);
            return View(realEstateProperties.ToList());
        }

        // GET: RealEstateProperties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealEstateProperty realEstateProperty = db.RealEstateProperties.Find(id);
            if (realEstateProperty == null)
            {
                return HttpNotFound();
            }
            return View(realEstateProperty);
        }

        // GET: RealEstateProperties/Create
        public ActionResult Create()
        {
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "name");
            SelectList htmlText = new SelectList(db.Categories, "Id", "name");
            if (Request.IsAjaxRequest())
            {
                //return PartialView("_create");
                return Content(JsonConvert.SerializeObject(htmlText, Formatting.Indented,
                            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            }
            else
            {
                return View();
            }
        }

        // POST: RealEstateProperties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,Address,Latitude,Longitude,OriginalSaleAmount,SoldAmount,SaleDate,CategoryId")] RealEstateProperty realEstateProperty)
        {
            if (ModelState.IsValid)
            {
                db.RealEstateProperties.Add(realEstateProperty);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    // reload book
                    var reloadeprop = db.RealEstateProperties
                                            .Include(x => x.Category)
                                            .FirstOrDefault(x => x.id == realEstateProperty.id);
                    return Content(JsonConvert.SerializeObject(reloadeprop, Formatting.Indented,
                            new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", realEstateProperty.CategoryId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_create", realEstateProperty);
            }
            else
            {
                return View(realEstateProperty);
            }
        }

        // GET: RealEstateProperties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealEstateProperty realEstateProperty = db.RealEstateProperties.Find(id);
            if (realEstateProperty == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "name", realEstateProperty.CategoryId);
            return View(realEstateProperty);
        }

        public ActionResult CustomEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealEstateProperty realEstateProperty = db.RealEstateProperties.Find(id);
            if (realEstateProperty == null)
            {
                return HttpNotFound();
            }
            var returnData =
            JsonConvert.SerializeObject(realEstateProperty, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Content(returnData);
        }

        [HttpGet]
        public ActionResult Sell(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealEstateProperty realEstateProperty = db.RealEstateProperties.Find(id);
            if (realEstateProperty == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "name", realEstateProperty.CategoryId);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_sellProperty", realEstateProperty);
            }
            else
            {
                return View(realEstateProperty);
            }
        }
        [HttpPost]
        public ActionResult Sell([Bind(Include = "id,Address,Latitude,Longitude,OriginalSaleAmount,SoldAmount,SaleDate,CategoryId")] RealEstateProperty realEstateProperty)
        {
            RealEstateProperty realEstatePropertyVal = db.RealEstateProperties.Find(realEstateProperty.id);
            realEstatePropertyVal.SaleDate = DateTime.Now;
            realEstatePropertyVal.SoldAmount = realEstateProperty.SoldAmount;
            if (ModelState.IsValid)
            {
                db.RealEstateProperties.AddOrUpdate(realEstatePropertyVal);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Property", db.RealEstateProperties.Find(realEstateProperty.id));
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "name", realEstateProperty.CategoryId);
            return View(realEstateProperty);
        }

        [HttpPost]
        public ActionResult CustomSell(int id, String SoldAmount)
        {
            RealEstateProperty realEstatePropertyVal = db.RealEstateProperties.Find(id);
            realEstatePropertyVal.SaleDate = DateTime.Now;
            realEstatePropertyVal.SoldAmount = ((String.IsNullOrEmpty(SoldAmount) || SoldAmount.Equals("null")) ? (decimal?)null : Convert.ToDecimal(SoldAmount));
            if (ModelState.IsValid)
            {
                db.RealEstateProperties.AddOrUpdate(realEstatePropertyVal);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Content("");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult CancelSell(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealEstateProperty realEstateProperty = db.RealEstateProperties.Find(id);
            if (realEstateProperty == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_property", realEstateProperty);
            }
            else
            {
                return HttpNotFound();
            }
        }
        // POST: RealEstateProperties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,Address,Latitude,Longitude,OriginalSaleAmount,SoldAmount,SaleDate,CategoryId")] RealEstateProperty realEstateProperty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realEstateProperty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "name", realEstateProperty.CategoryId);
            return View(realEstateProperty);
        }

        [HttpPost]
        public ActionResult CustomEditConfirm(int id, String Address, String Latitude, String Longitude, String OriginalSaleAmount , int CategoryId)
        {

            RealEstateProperty realEstateProperty = db.RealEstateProperties.Find(id);
            realEstateProperty.Address = Address;
            realEstateProperty.Latitude = ((String.IsNullOrEmpty(Latitude) || Latitude.Equals("null")) ? (double?)null : Convert.ToDouble(Latitude));
            realEstateProperty.Longitude = ((String.IsNullOrEmpty(Longitude) || Longitude.Equals("null")) ? (double?)null : Convert.ToDouble(Longitude));
            realEstateProperty.OriginalSaleAmount = ((String.IsNullOrEmpty(OriginalSaleAmount) || OriginalSaleAmount.Equals("null")) ? (decimal?)null : Convert.ToDecimal(OriginalSaleAmount));
            realEstateProperty.CategoryId = CategoryId;
            if (ModelState.IsValid)
            {
                db.RealEstateProperties.AddOrUpdate(realEstateProperty);
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Content("");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealEstateProperty realEstateProperty = db.RealEstateProperties.Find(id);
            if (realEstateProperty == null)
            {
                return HttpNotFound();
            }
            return View(realEstateProperty);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            RealEstateProperty rs = db.RealEstateProperties.Find(id);
            db.RealEstateProperties.Remove(rs);
            db.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Content("");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
