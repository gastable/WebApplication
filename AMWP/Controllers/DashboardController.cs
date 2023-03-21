using AMWP.Models;
using AMWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    [LoginCheck(type = 2)]
    public class DashboardController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();


        public ActionResult Dashboard(int id=24)
        {
            return View();
        }

        public ActionResult _GetAssetList(int id = 24)
        {
            GetAssets ga = new GetAssets();
            double sumPV = ga.GetTotalSecValue(id);
            double cashSum = ga.GetTotalCash(id);
            double ppySum = ga.GetTotalProValue(id);
            
            TempData["SecSum"] = sumPV;
            TempData["CashSum"] = cashSum;
            TempData["PropertySum"] = ppySum;
            return PartialView();
        }
    }
}