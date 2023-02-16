using AMWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace AMWP.Controllers
{
    public class MemberPropertiesController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: MemberProperties
        public ActionResult Display()
        {
            var properties = db.Properties.Include(p => p.Currencies).Include(p => p.Members);
            return View(properties.ToList());
        }
    }
}