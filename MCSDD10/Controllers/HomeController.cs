using MCSDD10.Models;
using MCSDD10.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MCSDD10.Controllers
{
    public class HomeController : Controller
    {
        MCSDD10Context db = new MCSDD10Context();

        [LoginCheck]
        public ActionResult Index()
        {
            var products = db.Products.Where(p => p.Discontinued == false).ToList();

            return View(products);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin vMLogin)
        {
            string password = BR.getHashPassword(vMLogin.Password);
            var member = db.Members.Where(m => m.Account == vMLogin.Account && m.Password == password).FirstOrDefault();

            if (member == null)
            {
                ViewBag.ErrMsg = "帳號或密碼錯誤！";
                return View(vMLogin);
            }
            Session["member"] = member;
            return RedirectToAction("Index");
        }

        public ActionResult Display(string id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = db.Products.Find(id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [LoginCheck(id =1)]
        public ActionResult Logout()
        {
            Session["member"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult MyCart()
        {
            return View();
        }
    }
}
