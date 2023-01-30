using _06ADOnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _06ADOnet.Controllers
{
    public class LoginController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string txtAccount,string txtPassword) //假設員工lastname是帳號firstname是密碼
        {
            var emp=db.Employees.Where(e => e.LastName == txtAccount && e.FirstName == txtPassword).FirstOrDefault();
            if(emp == null)
            {
                ViewBag.ErrMsg = "帳號或密碼錯誤";
                ViewBag.txtAccount= txtAccount;
                ViewBag.txtPassword= txtPassword;
                return View();
            }
            //若帳號密碼正確，則登入成功，跳轉至後台管理首頁
            Session["emp"]=emp;  //登入成功的狀態保留在Session物件中

            return RedirectToAction("Index","Customers");
        }
    }
}