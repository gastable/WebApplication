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
    public class WeeklyController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();
        public ActionResult Index(string order, int page = 1)
        {
            int pageSize = 15;
            int currentPage = page < 1 ? 1 : page;
            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(db.Weekly.Count()) / Convert.ToDouble(pageSize)));
            int skipRows = (currentPage - 1) * pageSize;
            var weekly = db.Weekly.OrderBy(d => d.SecID).ThenBy(w => w.Date);
            switch (order)
            {
                case "Symbol":
                    weekly = db.Weekly.OrderBy(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Symbol_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Date":
                    weekly = db.Weekly.OrderBy(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Date_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Open":
                    weekly = db.Weekly.OrderBy(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "Open_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "High":
                    weekly = db.Weekly.OrderBy(d => d.High).ThenBy(d => d.Date);
                    break;
                case "High_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.High).ThenBy(d => d.Date);
                    break;
                case "Low":
                    weekly = db.Weekly.OrderBy(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Low_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Close":
                    weekly = db.Weekly.OrderBy(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "Close_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "AdjClose":
                    weekly = db.Weekly.OrderBy(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "AdjClose_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "Dividend":
                    weekly = db.Weekly.OrderBy(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Dividend_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Volume":
                    weekly = db.Weekly.OrderBy(d => d.Volume).ThenBy(d => d.Date);
                    break;
                case "Volume_desc":
                    weekly = db.Weekly.OrderByDescending(d => d.Volume).ThenBy(d => d.Date);
                    break;
            }
            ViewBag.PageNumber = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.Order = order;
            var newWeekly = weekly.Skip(skipRows).Take(pageSize);
            return View(newWeekly);
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
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol");
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

            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", weekly.SecID);
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
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", weekly.SecID);
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
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", weekly.SecID);
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

        public JsonResult GetWeeklyData(string symbol, int num)
        {
            string sql = "select w.* from Weekly as w inner join Securities as s on w.SecID = s.SecID where s.Symbol=@symbol and w.[Date] between DATEADD(week,-@num,GETDATE()) and GETDATE() order by w.[Date]";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("symbol",symbol),
                 new SqlParameter("num",num)
            };
            DataTable dt = gd.TableQuery(sql, list);
            var timeseries = gd.GetTimeSeries();

            return Json(timeseries, JsonRequestBehavior.AllowGet);
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
