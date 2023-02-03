using _06ADOnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//要多加3個name sapce
using System.Data; //伺服器端的記憶體的Dataset的name sapce
using System.Data.SqlClient;  //伺服器端的硬碟中 SQL Server的資料庫name sapce
using System.Configuration;  //去讀Webconfig的連線字串

namespace _06ADOnet.Controllers
{
    public class LoginController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Login()
        {
            return View();
        }

    //    [HttpPost]
    //    public ActionResult Login(string txtAccount, string txtPassword) //假設員工lastname是帳號firstname是密碼
    //    {
    //        var emp = db.Employees.Where(e => e.LastName == txtAccount && e.FirstName == txtPassword).FirstOrDefault();
    //        if (emp == null)
    //        {
    //            ViewBag.ErrMsg = "帳號或密碼錯誤";
    //            ViewBag.txtAccount = txtAccount;  //保留輸入狀態
    //            ViewBag.txtPassword = txtPassword;
    //            return View();
    //        }
    //        //若帳號密碼正確，則登入成功，跳轉至後台管理首頁
    //        Session["emp"] = emp;  //登入成功的狀態保留在Session物件中
    //        return RedirectToAction("Index", "Customers");
    //    }
    //}

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin) //用Login的Action來示範怎麼寫ADO.net
        {
            //string sql = "select * from Employees where LastName='"+ vMLogin.Account + "' and FirstName='"+ vMLogin.Password + "'";
            //但是這種寫法會造成SQL Injection：帳號隨便打，只要在密碼欄位打「' or 1=1--」就能登入
            //應該用
            string sql = "select * from Employees where LastName=@id and FirstName=@pw";

            //建立SQL Parameters物件，參數(sql的參數名稱，值從model來取得)
            //SqlParameter id = new SqlParameter("id",vMLogin.Account);
            //SqlParameter pw = new SqlParameter("pw", vMLogin.Password);

            //但是不確定登入系統需要輸入幾個參數，所以用list泛型物來放比較合適
            List<SqlParameter> list = new List<SqlParameter>
            {
                new SqlParameter("id",vMLogin.Account),
                new SqlParameter("pw", vMLogin.Password)
            };

            //建立GetData物件，使用LoginQuery登入方法
            GetData gd = new GetData();   //如果controller內有很多Action要用，就在外面new
            
            var rd = gd.LoginQuery(sql, list);
            if(rd == null)
            {
                return View();
            }

            if (rd.HasRows)
            {
                Session["emp"] = rd;
                rd.Close();  //關閉讀取器，連帶關閉連線
                return RedirectToAction("Index", "Customers");
            }
           
            ViewBag.ErrMsg = "帳號或密碼錯誤";
            ViewBag.txtAccount = vMLogin.Account;
            ViewBag.txtPassword = vMLogin.Password;
            rd.Close();      //關閉讀取器，連帶關閉連線
            return View();            
        }

        public ActionResult Logout()
        {
            Session["emp"] = null;  //記錄為登出狀態

            return RedirectToAction("Login");
        }
    }
}