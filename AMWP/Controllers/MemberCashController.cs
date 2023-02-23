using AMWP.Models;
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
    public class MemberCashController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

        public ActionResult Display(int id = 24)
        {
            string sql = "select isnull(c.SSN,0) as SSN, cu.CCYID,cu.[Name], isnull(c.Amount,0) as Amount,cu.ExchRate, isnull(dbo.fnToMemCCY(@id)*C.Amount*Cu.ExchRate, 0) as ToCCY " +
                        "from Currencies as CU  left outer join(select * "+
                        "from cash where MemID = @id) as c "+
                        "on CU.CCYID = c.CCYID "+
                        "order by Cu.CCYID desc";
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
        public JsonResult GetMemberCashPie(int id=24)
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
            List<string> _labels = new List<string>();
            List<decimal> _data = new List<decimal>();

            foreach (DataRow row in ms.Rows)
            {
                _labels.Add(Convert.ToString(row["Name"]));
                _data.Add(Math.Round(Convert.ToDecimal(row["ToCCY"]), 2));
            };
            pie.labels = _labels;
            pie.data = _data;
            return Json(pie, JsonRequestBehavior.AllowGet);

        }

    }
}