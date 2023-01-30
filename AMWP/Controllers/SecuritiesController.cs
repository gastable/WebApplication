using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMWP.Models;

namespace AMWP.Controllers
{
    public class SecuritiesController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: Securities
        public ActionResult Index()
        {
            var securities = db.Securities.Include(s => s.Countries).Include(s => s.Currencies).Include(s => s.SecTypes);
            return View(securities.ToList());
        }

        // GET: Securities/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Securities securities = db.Securities.Find(id);
            if (securities == null)
            {
                return HttpNotFound();
            }
            return View(securities);
        }

        // GET: Securities/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name");
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name");
            return View();
        }

        // POST: Securities/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SecID,TypeID,CountryID,Symbol,Name,CCYID")] Securities securities)
        {
            var sec = db.Securities.Find(securities.SecID);
            if (sec != null)
            {
                ViewBag.PKCheck = "證券代碼重覆";
            }
            else if (ModelState.IsValid)
            {
                db.Securities.Add(securities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", securities.CountryID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", securities.CCYID);
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name", securities.TypeID);
            return View(securities);
        }

        // GET: Securities/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Securities securities = db.Securities.Find(id);
            if (securities == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", securities.CountryID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", securities.CCYID);
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name", securities.TypeID);
            return View(securities);
        }

        // POST: Securities/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecID,TypeID,CountryID,Symbol,Name,CCYID")] Securities securities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(securities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", securities.CountryID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", securities.CCYID);
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name", securities.TypeID);
            return View(securities);
        }

        // GET: Securities/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Securities securities = db.Securities.Find(id);
            if (securities == null)
            {
                return HttpNotFound();
            }
            db.Securities.Remove(securities);
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
