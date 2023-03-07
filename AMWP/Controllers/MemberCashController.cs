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

    }
}