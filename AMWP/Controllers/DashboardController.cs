using AMWP.Models;
using AMWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class DashboardController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        public ActionResult Dashboard(int id=22)
        {
            Dashboard dash = new Dashboard()
            {
                cash = db.Cash.Where(m => m.MemID == id).ToList(),
                countries = db.Countries.ToList(),
                currencies = db.Currencies.ToList(),
                daily = db.Daily.ToList(),
                members = db.Members.Where(m => m.MemID == id).ToList(),
                monthly= db.Monthly.ToList(),
                properties = db.Properties.Where(m => m.MemID == id).ToList(),
                securities = db.Securities.ToList(),
                secTypes = db.SecTypes.ToList(),
                secOrders = db.SecOrders.Where(m => m.MemID == id).ToList(),
                weekly = db.Weekly.ToList()
            };

            return View(dash);
        }
    }
}