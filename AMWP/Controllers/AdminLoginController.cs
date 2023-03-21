using AMWP.Models;
using AMWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class AdminLoginController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        // GET: AdminLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLogin login)
        {
            string sql = "select * from Admins where Account=@id and Password=@pw";
            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("id",login.Account),
                new SqlParameter("pw",login.Password)
            };
            GetData gd = new GetData();

            var rd = gd.LoginQuery(sql, list);

            if (rd == null)
            {
                return View();
            }

            if (rd.HasRows)
            {
                Session["admin"] = rd;
                rd.Close();  
                return RedirectToAction("Index", "Admins");
            }

            ViewBag.ErrMsg = "帳號或密碼錯誤";
            ViewBag.txtAccount = login.Account;
            ViewBag.txtPassword = login.Password;
            rd.Close();      //關閉讀取器，連帶關閉連線
            return View();
        }

        [LoginCheck]
        public ActionResult Logout()
        {
            Session["admin"] = null;  //記錄為登出狀態

            return RedirectToAction("Login");
        }
    }
}