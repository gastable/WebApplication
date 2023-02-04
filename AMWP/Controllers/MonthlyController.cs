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
    public class MonthlyController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: Monthly
        public ActionResult Index()
        {
            var monthly = db.Monthly.Include(m => m.Securities);
            return View(monthly.ToList());
        }

        // GET: Monthly/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monthly monthly = db.Monthly.Find(id);
            if (monthly == null)
            {
                return HttpNotFound();
            }
            return View(monthly);
        }

        // GET: Monthly/Create
        public ActionResult Create()
        {
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol");
            return View();
        }

        // POST: Monthly/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,SecID,Date,Open,High,Low,Close,AdjClose,Dividend,Volume")] Monthly monthly)
        {
            if (ModelState.IsValid)
            {
                db.Monthly.Add(monthly);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", monthly.SecID);
            return View(monthly);
        }

        // GET: Monthly/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monthly monthly = db.Monthly.Find(id);
            if (monthly == null)
            {
                return HttpNotFound();
            }
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", monthly.SecID);
            return View(monthly);
        }

        // POST: Monthly/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,SecID,Date,Open,High,Low,Close,AdjClose,Dividend,Volume")] Monthly monthly)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthly).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", monthly.SecID);
            return View(monthly);
        }

        // GET: Monthly/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monthly monthly = db.Monthly.Find(id);
            if (monthly == null)
            {
                return HttpNotFound();
            }
            return View(monthly);
        }

        // POST: Monthly/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Monthly monthly = db.Monthly.Find(id);
            db.Monthly.Remove(monthly);
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
