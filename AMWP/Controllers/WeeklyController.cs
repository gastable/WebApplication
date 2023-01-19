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
    public class WeeklyController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: Weekly
        public ActionResult Index()
        {
            var weekly = db.Weekly.Include(w => w.Securities);
            return View(weekly.ToList());
        }

        // GET: Weekly/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekly weekly = db.Weekly.Find(id);
            if (weekly == null)
            {
                return HttpNotFound();
            }
            return View(weekly);
        }

        // GET: Weekly/Create
        public ActionResult Create()
        {
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "TypeID");
            return View();
        }

        // POST: Weekly/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,SecID,Date,Open,High,Low,Close,AdjClose,Dividend,Volume")] Weekly weekly)
        {
            if (ModelState.IsValid)
            {
                db.Weekly.Add(weekly);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SecID = new SelectList(db.Securities, "SecID", "TypeID", weekly.SecID);
            return View(weekly);
        }

        // GET: Weekly/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekly weekly = db.Weekly.Find(id);
            if (weekly == null)
            {
                return HttpNotFound();
            }
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "TypeID", weekly.SecID);
            return View(weekly);
        }

        // POST: Weekly/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,SecID,Date,Open,High,Low,Close,AdjClose,Dividend,Volume")] Weekly weekly)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weekly).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "TypeID", weekly.SecID);
            return View(weekly);
        }

        // GET: Weekly/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weekly weekly = db.Weekly.Find(id);
            if (weekly == null)
            {
                return HttpNotFound();
            }
            return View(weekly);
        }

        // POST: Weekly/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Weekly weekly = db.Weekly.Find(id);
            db.Weekly.Remove(weekly);
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
