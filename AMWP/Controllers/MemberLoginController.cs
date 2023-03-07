using AMWP.ViewModels;
using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class MemberLoginController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberLogin memberLogin)
        {
            string sql = "select * from Members where Account=@id and Password=@pw";
            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("id",memberLogin.Account),
                new SqlParameter("pw",memberLogin.Password)
            };
            GetData gd = new GetData();

            var rd = gd.LoginQuery(sql, list);

            if (rd == null)
            {
                return View();
            }

            if (rd.HasRows)
            {
                if (Convert.ToBoolean(rd["Status"]) == true)
                {
                    Session["mem"] = rd["MemID"];
                    Session["CCY"] = rd["CCYID"];
                    rd.Close();  
                    return RedirectToAction("Dashboard", "Dashboard", new {id= Session["mem"],ccy= Session["CCY"] });
                }
                ViewBag.ErrMsg = "會員帳號已封鎖！";                
            }
            else
            {
            ViewBag.ErrMsg = "帳號或密碼錯誤！";
            }
            ViewBag.txtAccount = memberLogin.Account;
            ViewBag.txtPassword = memberLogin.Password;
            rd.Close();      //關閉讀取器，連帶關閉連線
            return View();
        }

        public ActionResult Logout()
        {
            Session["mem"] = null;  //記錄為訪客狀態
            Session["CCY"] = null;
            Session["secSum"] = null;
            Session["cashSum"] = null;
            Session["pptySum"] = null;

            return RedirectToAction("Index","Home");
        }
    }
}