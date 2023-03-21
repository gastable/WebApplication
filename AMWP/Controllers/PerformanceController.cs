using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    [LoginCheck(type = 2)]
    public class PerformanceController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();
        public ActionResult Display(string symbol)
        {
            ViewBag.symbol = symbol;
            return View();
        }


        public JsonResult GetDailyData(string symbol, int num)
        {
            string sql = "select d.* from daily as d inner join Securities as s on d.SecID = s.SecID where s.Symbol=@symbol and d.[Date] between DATEADD(day,-@num,GETDATE()) and GETDATE() order by d.[Date]";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("symbol",symbol),
                 new SqlParameter("num",num)
            };
            DataTable dt = gd.TableQuery(sql, list);
            var timeseries = gd.GetTimeSeries();

            return Json(timeseries, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetMonthlyData(string symbol, int num)
        {
            string sql = "select m.* from Monthly as m inner join Securities as s on m.SecID = s.SecID where s.Symbol=@symbol and m.[Date] between DATEADD(month,-@num,GETDATE()) and GETDATE() order by m.[Date]";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("symbol",symbol),
                 new SqlParameter("num",num)
            };
            DataTable dt = gd.TableQuery(sql, list);
            var timeseries = gd.GetTimeSeries();

            return Json(timeseries, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string symbol,string order, int page = 1)
        {
            int pageSize = 15;
            int currentPage = page < 1 ? 1 : page;
            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(db.Weekly.Count()) / Convert.ToDouble(pageSize)));
            int skipRows = (currentPage - 1) * pageSize;
            var weekly = db.Weekly.Where(w=>w.Securities.Symbol==symbol).OrderBy(w => w.SecID).ThenBy(w => w.Date);
            switch (order)
            {
                case "Symbol":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Symbol_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.Securities.Symbol).ThenBy(d => d.Date);
                    break;
                case "Date":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Date_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.Date).ThenBy(d => d.Securities.Symbol);
                    break;
                case "Open":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "Open_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.Open).ThenBy(d => d.Date);
                    break;
                case "High":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.High).ThenBy(d => d.Date);
                    break;
                case "High_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.High).ThenBy(d => d.Date);
                    break;
                case "Low":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Low_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.Low).ThenBy(d => d.Date);
                    break;
                case "Close":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "Close_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.Close).ThenBy(d => d.Date);
                    break;
                case "AdjClose":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "AdjClose_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.AdjClose).ThenBy(d => d.Date);
                    break;
                case "Dividend":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Dividend_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.Dividend).ThenBy(d => d.Date);
                    break;
                case "Volume":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderBy(d => d.Volume).ThenBy(d => d.Date);
                    break;
                case "Volume_desc":
                    weekly = db.Weekly.Where(w => w.Securities.Symbol == symbol).OrderByDescending(d => d.Volume).ThenBy(d => d.Date);
                    break;
            }
            ViewBag.PageNumber = currentPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.Order = order;
            ViewBag.Symbol = symbol;
            var newWeekly = weekly.Skip(skipRows).Take(pageSize);
            return View(newWeekly);
        }

    }
}