using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class MemberCashController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

        public ActionResult Display(int id = 22)
        {
            string sql = "SELECT Cash.CCYID, Cash.Amount, Currencies.ExchRate, dbo.fnToMemCCY(@id)*Cash.Amount*Currencies.ExchRate as ToCCY "+ 
                         "FROM Cash INNER JOIN Currencies ON Cash.CCYID = Currencies.CCYID "+
                         "where Cash.MemID =@id";
            List<SqlParameter> list = new List<SqlParameter>() {
                    new SqlParameter("id",id)
            };

            var cash = gd.TableQuery(sql, list);
            if (cash == null)
            {
                return View();
            }
            if(cash.Rows.Count == 0)
            {
                ViewBag.CashMsg = "您目前無現金庫存資料！";               
            }
            ViewBag.MemID = id;
            return View(cash);
        }
        //ViewBag.ErrMsg = "增加現金紀錄？";
        //return View();
    }
}