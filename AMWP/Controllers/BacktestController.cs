using AMWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class BacktestController : Controller
    {
        // GET: Backtest
        public ActionResult Input()
        {
            return View();
        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult Input(Backtest backtest)
        {

            return View(backtest);
        }

        public ActionResult _Display()
        {
            return PartialView();
        }
    }
}