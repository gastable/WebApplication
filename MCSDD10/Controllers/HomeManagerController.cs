using MCSDD10.Models;
using MCSDD10.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCSDD10.Controllers
{
    public class HomeManagerController : Controller
    {
        MCSDD10Context db = new MCSDD10Context();

        [LoginCheck]
        // GET: HomeManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin)
        {
            string password = BR.getHashPassword( vMLogin.Password);
            var user = db.Employess.Where(m=>m.Account==vMLogin.Account && m.Password== password ).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrMsg = "帳號或密碼錯誤！";
                return View(vMLogin);
            }
            Session["user"] = user;
            
            return RedirectToAction("Index");
        }

        [LoginCheck]
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login");
        }
    }
}