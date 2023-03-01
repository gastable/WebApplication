using AMWP.Models;
using AMWP.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


namespace AMWP.Controllers
{
    public class MemberSecuritiesController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();
                

        public ActionResult DisplaySecOrders(int id = 24)
        {
            string sql = "SELECT SecOrders.SSN,SecOrders.Date, SecOrders.TrancType, Securities.Symbol, SecOrders.Share, SecOrders.Price, SecOrders.Fee, Securities.TypeID " +
                         "FROM SecOrders INNER JOIN Securities ON SecOrders.SecID = Securities.SecID " +
                         "Where SecOrders.MemID = @id "+
                         "order by SecOrders.Date,SecOrders.Price,SecOrders.TrancType";

            List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter("id",id)
            };

            var securities = gd.TableQuery(sql, list);
            if (securities == null)
            {
                return View();
            }
            ViewBag.MemID = id;
            return View(securities);

            //ViewBag.ErrMsg = "增加現金紀錄？";
            //return View();
        }

        public ActionResult _GetMemberSecList(int id = 24) {
            string sql = "queryMemberSecurities";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            var ms = gd.TableQueryBySP(sql, list);
            return PartialView(ms);
        }

        public ActionResult GetValueLineChart(int id = 24)
        {
            string sql = "queryMemberSecValues";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            DataTable dt = gd.TableQueryBySP(sql, list);

            Chart lineChart = new Chart();
            List<string> labels = new List<string>();
            List<double> data = new List<double>();

            foreach (DataRow row in dt.Rows)
            {
                labels.Add(Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd"));
                data.Add(Math.Round(Convert.ToDouble(row["Value"]), 2));
            };
            lineChart.Labels = labels;
            lineChart.Data = data;
            return Json(lineChart, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSecPieChart(int id = 24)
        {
            string sql = "queryMemberSecurities";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("id",id)
            };
            DataTable ms = gd.TableQueryBySP(sql, list);

            Chart pieChart = new Chart();
            List<string> labels = new List<string>();
            List<double> data = new List<double>();

            foreach (DataRow row in ms.Rows)
            {
                labels.Add(Convert.ToString(row["Symbol"]));
                data.Add(Math.Round(Convert.ToDouble(row["Close"]) * Convert.ToDouble(row["Share"]) * Convert.ToDouble(row["ExchRate"]) * Convert.ToDouble(row["ToCCY"]), 2));
            };
            pieChart.Labels = labels;
            pieChart.Data = data;
            return Json(pieChart, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSecTypeDoughnutChart(int id = 24)
        {
            string sql = "queryMemberSecType";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("id",id)
            };
            DataTable ms = gd.TableQueryBySP(sql, list);

            Chart doughnutChart = new Chart();
            List<string> labels = new List<string>();
            List<double> data = new List<double>();

            foreach (DataRow row in ms.Rows)
            {
                labels.Add(Convert.ToString(row["Name"]));
                data.Add(Math.Round(Convert.ToDouble(row["Value"]) , 2));
            };
            doughnutChart.Labels = labels;
            doughnutChart.Data = data;
            return Json(doughnutChart, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _GetSecTypeList(int id = 24)
        {
            string sql = "queryMemberSecType";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            var ms = gd.TableQueryBySP(sql, list);
            return PartialView(ms);
        }
    }
}