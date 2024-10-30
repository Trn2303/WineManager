using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WineManager.Models;

namespace WineManager.Controllers
{
    public class ProductsController : Controller
    {
        private WineStoreDB db = new WineStoreDB();

        // GET: Products
        public ActionResult Index(string productType, int? vintage, string region, int? page)
        {
            productType = productType?.Trim();
            region = region?.Trim();

            var products = db.Product.AsQueryable();

            // Load data for dropdowns
            ViewBag.ProductTypes = db.Catalogy.Select(c => new { c.CatalogyID, c.CatalogyName }).Distinct().ToList();
            ViewBag.Vintages = db.Product.Select(p => p.Vintage).Distinct().OrderByDescending(y => y).ToList();
            ViewBag.Regions = db.Product.Select(p => p.Region).Distinct().ToList();

            if (!string.IsNullOrWhiteSpace(productType))
            {
                products = products.Where(p => p.CatalogyID == productType);
                ViewBag.CurrentProductType = productType;
            }

            if (vintage.HasValue)
            {
                products = products.Where(p => p.Vintage == vintage.ToString());
                ViewBag.CurrentVintage = vintage;
            }

            if (!string.IsNullOrEmpty(region))
            {
                products = products.Where(p => p.Region == region);
                ViewBag.CurrentRegion = region;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(products.OrderBy(p => p.ProductName).ToPagedList(pageNumber, pageSize));
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Description,PurchasePrice,Price,Quantity,Vintage,CatalogyID,Image,Region")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Product.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập file ảnh! " + ex.Message;
                ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName", product.CatalogyID);
                return View(product);
            }
            
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName", product.CatalogyID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Description,PurchasePrice,Price,Quantity,Vintage,CatalogyID,Image,Region")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImgFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadFile = Server.MapPath("~/wwwroot/WineImages/" + FileName);
                        f.SaveAs(UploadFile);
                        product.Image = FileName;
                    }
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.CatalogyID = new SelectList(db.Catalogy, "CatalogyID", "CatalogyName", product.CatalogyID);
                return View(product);
            }
            
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
