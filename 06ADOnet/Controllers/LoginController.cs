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

        //[HttpPost]
        //public ActionResult Login(VMLogin vMLogin) //假設員工lastname是帳號firstname是密碼
        //{
        //    var emp=db.Employees.Where(e => e.LastName == vMLogin.Account && e.FirstName == vMLogin.Password).FirstOrDefault();
        //    if(emp == null)
        //    {
        //        ViewBag.ErrMsg = "帳號或密碼錯誤";
        //        ViewBag.txtAccount= vMLogin.Account;
        //        ViewBag.txtPassword= vMLogin.Password;
        //        return View();
        //    }
        //    //若帳號密碼正確，則登入成功，跳轉至後台管理首頁
        //    Session["emp"]=emp;  //登入成功的狀態保留在Session物件中

        //    return RedirectToAction("Index","Customers");
        //}

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin) //用Login的Action來示範怎麼寫ADO.net
        {
            string sql = "select * from Employees where LastName='"+ vMLogin.Account + "' and FirstName='"+ vMLogin.Password + "'";
            //但是這種寫法會造成SQL Injection：帳號隨便打，只要在密碼欄位打「' or 1=1--」就能登入

            //1.建立資料庫連線物件
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString); 
            //運用組態管理員物件去找ConnectionStrings，再用name當索引後，找到屬性ConnectionString的值
            
            //2.建立SQL命令物件
            SqlCommand cmd = new SqlCommand(sql,conn); //最少給2個屬性參數：sql指令字串(提出去用sql指定)，連線物件
            
            //3.建立資料讀取物件
            SqlDataReader rd; //先不初始化，因為下面要寫參數

            conn.Open();//打開連線，另外記得後面要再先寫Close()
            rd = cmd.ExecuteReader();//讀取cmd執行後的資料放入rd，
            if (rd.Read())   //目前只會讀1筆，如果多筆資料就放while迴圈巡覽
            {
                Session["emp"] = rd[0];  //讀第1筆資料放入Session
                conn.Close();
                return RedirectToAction("Index", "Customers");
            }
                ViewBag.ErrMsg = "帳號或密碼錯誤";
                ViewBag.txtAccount = vMLogin.Account;
                ViewBag.txtPassword = vMLogin.Password;
                conn.Close();
                return View();            
        }

        public ActionResult Logout()
        {
            Session["emp"] = null;  //記錄為登出狀態

            return RedirectToAction("Login");
        }
    }
}