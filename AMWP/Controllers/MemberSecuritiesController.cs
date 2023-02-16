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
                

        public ActionResult DisplaySecOrders(int id = 22)
        {
            string sql = "SELECT SecOrders.Date, SecOrders.TrancType, Securities.Symbol, SecOrders.Share, SecOrders.Price, SecOrders.Fee " +
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

        public ActionResult _GetMemberSecList(int id = 22) {
            string sql = "queryMemberSecurities";
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("id",id)
            };
            var ms = gd.TableQueryBySP(sql, list);
            return PartialView(ms);
        }

        public JsonResult GetMemberSecPie(int id = 22)
        {
            string sql = "queryMemberSecurities";
            List<SqlParameter> list = new List<SqlParameter> {
                 new SqlParameter("id",id)
            };
            DataTable ms = gd.TableQueryBySP(sql, list);

            Chart pie = new Chart();
            List<string> _labels = new List<string>();
            List<decimal> _data = new List<decimal>();

            foreach (DataRow row in ms.Rows)
            {
                _labels.Add(Convert.ToString(row["Symbol"]));
                _data.Add(Math.Round(Convert.ToDecimal(row["Close"]) * Convert.ToDecimal(row["Share"]) * Convert.ToDecimal(row["ExchRate"]) * Convert.ToDecimal(row["ToCCY"]), 2));                               
            };
            pie.labels = _labels;
            pie.data = _data;
            return Json(pie, JsonRequestBehavior.AllowGet);            
            
        }

       


    }
}