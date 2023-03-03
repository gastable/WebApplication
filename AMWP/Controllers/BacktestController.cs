using AMWP.Models;
using AMWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers{

    public class BacktestController : Controller
    {
        AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

        // GET: Backtest
        public ActionResult Input()
        {           
            return View();
        }

        [HttpPost]
        //[ChildActionOnly]
        public ActionResult Input(Backtest backtests)
        {
            DateTime startDate = Convert.ToDateTime(backtests.StartYear + "-01-01");
            DateTime endDate = Convert.ToDateTime(backtests.EndYear + "-12-31");
            
            return View(backtests);
        }

        public ActionResult _Display()
        {
            return PartialView();
        }
    }
}