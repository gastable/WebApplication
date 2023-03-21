using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMWP.Models;

namespace AMWP.Controllers
{
    [LoginCheck]
    public class DailyController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

        // GET: Weekly
        public ActionResult Index(string order, int page = 1)
        {
            int pageSize = 15;
            int currentPage = page < 1 ? 1 : page;            
            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(db.Daily.Count())/ Convert.ToDouble(pageSize)));
            int skipRows = (currentPage - 1) * pageSize;            
            var daily = db.Daily.OrderBy(d => d.SecID).ThenBy(w => w.Date);
            switch (order)
            {
                case "Symbol":
                    daily = db.Daily.OrderBy(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Symbol_desc":
                    daily = db.Daily.OrderByDescending(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Date":
                    daily = db.Daily.OrderBy(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Date_desc":
                    daily = db.Daily.OrderByDescending(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Open":
                    daily = db.Daily.OrderBy(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "Open_desc":
                    daily = db.Daily.OrderByDescending(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "High":
                    daily = db.Daily.OrderBy(d => d.High).ThenBy(d => d.Date);
                    break;
                case "High_desc":
                    daily = db.Daily.OrderByDescending(d => d.High).ThenBy(d => d.Date);
                    break;
                case "Low":
                    daily = db.Daily.OrderBy(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Low_desc":
                    daily = db.Daily.OrderByDescending(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Close":
                    daily = db.Daily.OrderBy(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "Close_desc":
                    daily = db.Daily.OrderByDescending(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "AdjClose":
                    daily = db.Daily.OrderBy(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "AdjClose_desc":
                    daily = db.Daily.OrderByDescending(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "Dividend":
                    daily = db.Daily.OrderBy(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Dividend_desc":
                    daily = db.Daily.OrderByDescending(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Volume":
                    daily = db.Daily.OrderBy(d => d.Volume).ThenBy(d => d.Date);
                    break;
                case "Volume_desc":
                    daily = db.Daily.OrderByDescending(d => d.Volume).ThenBy(d => d.Date);
                    break;
            }
            ViewBag.PageNumber = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.Order = order;
            var newDaily = daily.Skip(skipRows).Take(pageSize);
            return View(newDaily);
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
