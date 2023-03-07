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

    }
}