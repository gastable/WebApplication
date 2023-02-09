using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class PerformanceController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        public ActionResult Display()
        {
           return View();
        }
    }
}