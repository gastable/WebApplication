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

        public ActionResult Index(string order, int page = 1)
        {
            int pageSize = 15;
            int currentPage = page < 1 ? 1 : page;
            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(db.Monthly.Count()) / Convert.ToDouble(pageSize)));
            int skipRows = (currentPage - 1) * pageSize;
            var monthly = db.Monthly.OrderBy(d => d.SecID).ThenBy(w => w.Date);
            switch (order)
            {
                case "Symbol":
                    monthly = db.Monthly.OrderBy(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Symbol_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Date":
                    monthly = db.Monthly.OrderBy(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Date_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Open":
                    monthly = db.Monthly.OrderBy(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "Open_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "High":
                    monthly = db.Monthly.OrderBy(d => d.High).ThenBy(d => d.Date);
                    break;
                case "High_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.High).ThenBy(d => d.Date);
                    break;
                case "Low":
                    monthly = db.Monthly.OrderBy(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Low_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Close":
                    monthly = db.Monthly.OrderBy(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "Close_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "AdjClose":
                    monthly = db.Monthly.OrderBy(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "AdjClose_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "Dividend":
                    monthly = db.Monthly.OrderBy(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Dividend_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Volume":
                    monthly = db.Monthly.OrderBy(d => d.Volume).ThenBy(d => d.Date);
                    break;
                case "Volume_desc":
                    monthly = db.Monthly.OrderByDescending(d => d.Volume).ThenBy(d => d.Date);
                    break;
            }
            ViewBag.PageNumber = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.Order = order;
            var newMonthly = monthly.Skip(skipRows).Take(pageSize);
            return View(newMonthly);
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
