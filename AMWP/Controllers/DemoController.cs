using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class DemoController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();
        int id = 24;

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult _GetAssetList()
        {
            GetAssets ga = new GetAssets();
            double sumPV = ga.GetTotalSecValue(id);
            double cashSum = ga.GetTotalCash(id);
            double ppySum = ga.GetTotalProValue(id);

            TempData["SecSum"] = sumPV;
            TempData["CashSum"] = cashSum;
            TempData["PropertySum"] = ppySum;
            return PartialView();
        }

        public ActionResult _GetSecTypeList()
        {
            string sql = "queryMemberSecType";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            var ms = gd.TableQueryBySP(sql, list);
            return PartialView(ms);
        }

        public ActionResult DisplaySecOrders()
        {
            string sql = "SELECT SecOrders.SSN,SecOrders.Date, SecOrders.TrancType, Securities.Symbol, SecOrders.Share, SecOrders.Price, SecOrders.Fee, Securities.TypeID " +
                         "FROM SecOrders INNER JOIN Securities ON SecOrders.SecID = Securities.SecID " +
                         "Where SecOrders.MemID = @id " +
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

        public ActionResult _GetMemberSecList()
        {
            string sql = "queryMemberSecurities";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            var ms = gd.TableQueryBySP(sql, list);
            return PartialView(ms);
        }

        public ActionResult GetValueLineChart()
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

        public JsonResult GetSecPieChart()
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

        public JsonResult GetSecTypeDoughnutChart()
        {
            string sql = "queryMemberSecType";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("id",id)
            };
            DataTable ms = gd.TableQueryBySP(sql, list);

            Chart doughnutChart = new Chart();
            List<string> labels = new List<string>();
            List<double> data = new List<double>();
            List<string> typeIds = new List<string>();

            foreach (DataRow row in ms.Rows)
            {
                labels.Add(Convert.ToString(row["Name"]));
                data.Add(Math.Round(Convert.ToDouble(row["Value"]), 2));
                typeIds.Add(Convert.ToString(row["TypeID"]));
            };
            doughnutChart.Labels = labels;
            doughnutChart.Data = data;
            doughnutChart.Data3 = typeIds;
            return Json(doughnutChart, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DisplayCash()
        {
            string sql = "select isnull(c.SSN,0) as SSN, cu.CCYID,cu.[Name], isnull(c.Amount,0) as Amount,cu.ExchRate, isnull(dbo.fnToMemCCY(@id)*C.Amount*Cu.ExchRate, 0) as ToCCY " +
                        "from Currencies as CU  left outer join(select * " +
                        "from cash where MemID = @id) as c " +
                        "on CU.CCYID = c.CCYID " +
                        "order by Cu.CCYID desc";
            List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter("id",id)
            };

            DataTable cash = gd.TableQuery(sql, list);
            if (cash == null)
            {
                return View();
            }
            double cashSum = 0;
            foreach (DataRow row in cash.Rows)
            {
                cashSum += Convert.ToDouble(row["ToCCY"]);
            }
            if (cashSum == 0)
            {
                ViewBag.CashMsg = "您目前無任何現金庫存！";
            }
            ViewBag.MemID = id;
            return View(cash);

        }
        public JsonResult GetMemberCashPie()
        {
            string sql = "select isnull(c.SSN,0) as SSN, cu.CCYID,cu.[Name], isnull(c.Amount,0) as Amount,cu.ExchRate, isnull(dbo.fnToMemCCY(@id)*C.Amount*Cu.ExchRate, 0) as ToCCY " +
                        "from Currencies as CU  left outer join(select * " +
                        "from cash where MemID = @id) as c " +
                        "on CU.CCYID = c.CCYID " +
                        "order by Cu.CCYID desc";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("id",id)
            };
            DataTable ms = gd.TableQuery(sql, list);

            Chart pie = new Chart();
            List<string> labels = new List<string>();
            List<double> data = new List<double>();

            foreach (DataRow row in ms.Rows)
            {
                labels.Add(Convert.ToString(row["Name"]));
                data.Add(Math.Round(Convert.ToDouble(row["ToCCY"]), 2));
            };
            pie.Labels = labels;
            pie.Data = data;
            return Json(pie, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DisplayProperties()
        {
            string sql = "select p.SSN, p.[Name] as 房產名稱,p.[Date] as 貸款日期,p.Price as 房產估值,p.Loan as 貸款金額,p.Term as 貸款期數,p.Principal as 每月還本金額,p.PayDay as 每月還款日,c.[Name] as 貸款幣別 from Properties as p inner join Currencies as c on p.CCYID=c.CCYID where MemID=@id";
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter("id", id)
            };
            var mp = gd.TableQuery(sql, list);
            if (mp == null)
            {
                return View();
            }
            if (mp.Rows.Count == 0)
            {
                ViewBag.Msg = "您目前無房產資料！";
            }
            ViewBag.MemID = id;
            return View(mp);
        }

        public ActionResult GetPropertyNetValue()
        {
            var properties = db.Properties.Where(p => p.MemID == id);
            Chart lieChart = new Chart();
            List<string> labels = new List<string>();
            List<double> data = new List<double>();
            List<double> data2 = new List<double>();

            return View(properties);
        }
    }
}