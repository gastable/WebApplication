using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AMWP.Models;
using PagedList;

namespace AMWP.Controllers
{
    public class DailyController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

        int pageSize = 15;
        // GET: Daily
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var daily = db.Daily.OrderBy(w => w.SecID).ThenBy(w => w.Date);
            var result = daily.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        // GET: Daily/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Daily daily = db.Daily.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // GET: Daily/Create
        public ActionResult Create()
        {
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol");
            return View();
        }

        // POST: Daily/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,SecID,Date,Open,High,Low,Close,AdjClose,Dividend,Volume")] Daily daily)
        {
            if (ModelState.IsValid)
            {
                db.Daily.Add(daily);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", daily.SecID);
            return View(daily);
        }

        // GET: Daily/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Daily daily = db.Daily.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", daily.SecID);
            return View(daily);
        }

        // POST: Daily/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,SecID,Date,Open,High,Low,Close,AdjClose,Dividend,Volume")] Daily daily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daily).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", daily.SecID);
            return View(daily);
        }

        // GET: Daily/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Daily daily = db.Daily.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // POST: Daily/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Daily daily = db.Daily.Find(id);
            db.Daily.Remove(daily);
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
