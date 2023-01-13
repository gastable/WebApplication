using _05CustomValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _05CustomValidation.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Members members)
        {
            return View();
        }
    }
}