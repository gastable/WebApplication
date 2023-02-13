using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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
            string sql = "SELECT SecOrders.[Date], SecOrders.TrancType, Securities.Symbol, SecOrders.Share, SecOrders.Price, SecOrders.Fee " +
                         "FROM SecOrders INNER JOIN Securities ON SecOrders.SecID = Securities.SecID " +
                         "Where SecOrders.MemID = @id";

            List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter("id",id)
            };

            var securities = gd.TableQuery(sql, list);
            if (securities == null)
            {
                return View();
            }
            if (securities.Rows.Count == 0)
            {
                ViewBag.CashMsg = "您目前無現金庫存資料！";
            }
            ViewBag.MemID = id;
            return View(securities);

            //ViewBag.ErrMsg = "增加現金紀錄？";
            //return View();
        }
    }
}