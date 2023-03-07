using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMWP.Models;
using Newtonsoft.Json;
using ServiceStack;

namespace AMWP.Controllers
{
    public class SecuritiesController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

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
            try
            {
                db.Securities.Remove(securities);
                db.SaveChanges();
            }
            catch
            {
                TempData["ForeignKey"] = "已有會員交易此證券，無法刪除！";
            }
            return RedirectToAction("Index");
        }

     
        
        public ActionResult UploadAPI(string interval,string symbol,string secid,string sqlSP)
        {
            var api = $"https://www.alphavantage.co/query?function=TIME_SERIES_{interval}&symbol={symbol}&apikey=XBRCUGRFRDK9P0HJ&datatype=csv";
            GetAlphaData gad = new GetAlphaData();
            SetData sd = new SetData();
            try
            {
                gad.TimeSeries(api);
                string sql = sqlSP;
                List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("secid",secid)
                };
                sd.executeSP(sql,list);
                TempData["uploads"] = symbol + "的" + interval.Substring(0, interval.IndexOf("_")) + "資料已更新！";
            }
            catch
            {
                TempData["uploads"] = symbol + "的" + interval.Substring(0, interval.IndexOf("_")) + "資料更新失敗！";
            }

            return RedirectToAction("Index");
        }

        public ActionResult UploadAPIWM(string interval, string symbol, string secid, string sqlSP)
        {
            var api = $"https://www.alphavantage.co/query?function=TIME_SERIES_{interval}&symbol={symbol}&apikey=XBRCUGRFRDK9P0HJ&datatype=csv";
            GetAlphaData gad = new GetAlphaData();
            SetData sd = new SetData();
            try
            {
                gad.TimeSeriesWM(api);
                string sql = sqlSP;
                List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("secid",secid)
                };

                sd.executeSP(sql, list);
                TempData["uploads"] = symbol + "的" + interval.Substring(0, interval.IndexOf("_")) + "資料已更新！";
            }
            catch
            {
                TempData["uploads"] = symbol + "的" + interval.Substring(0, interval.IndexOf("_")) + "資料更新失敗！";
            }

            return RedirectToAction("Index");
        }

        public JsonResult SearchSymbol(string query)
        {
            query += "%";
            string sql = "select Symbol from Securities where Symbol like @query";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("query",query)
            };
            DataTable ms = gd.TableQuery(sql, list);
            List<string> symbols = new List<string>();
            foreach(DataRow row in ms.Rows)
            {
                symbols.Add(Convert.ToString(row["Symbol"]));
            }
            return Json(symbols, JsonRequestBehavior.AllowGet);
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
