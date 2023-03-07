using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class MemberPropertiesController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();
        // GET: MemberProperties
        public ActionResult Display(int id = 24)
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

        public ActionResult GetPropertyNetValue(int id = 24)
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