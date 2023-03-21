using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMWP.Models;

namespace AMWP.Controllers
{
    [LoginCheck(type =2)]
    public class CashController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        [LoginCheck]
        // GET: Cash
        public ActionResult Index()
        {
            var cash = db.Cash.Include(c => c.Members).Include(c => c.Currencies);
            return View(cash.ToList());
        }

        // GET: Cash/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cash cash = db.Cash.Find(id);
            if (cash == null)
            {
                return HttpNotFound();
            }
            return View(cash);
        }

        // GET: Cash/Create
        public ActionResult _Create(string ccyid,string name)
        {
            
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account");
            ViewBag.CCYID = ccyid;
            ViewBag.Name = name;
            TempData["CCYName"] = name;
            return PartialView();
        }

        // POST: Cash/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "SSN,MemID,CCYID,Amount")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                
                db.Cash.Add(cash);
                db.SaveChanges();
                return RedirectToAction("Display", "MemberCash", new {id= cash.MemID });
            }

            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", cash.MemID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", cash.CCYID);
            return PartialView(cash);
        }

        // GET: Cash/Edit/5
        public ActionResult _Edit(int? id, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cash cash = db.Cash.Find(id);
            if (cash == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", cash.MemID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", cash.CCYID);
            ViewBag.Name = name;
            return PartialView(cash);
        }

        // POST: Cash/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit([Bind(Include = "SSN,MemID,CCYID,Amount")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Display", "MemberCash", new { id = cash.MemID });
            }
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", cash.MemID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", cash.CCYID);
            return RedirectToAction("Display", "MemberCash", new { id = cash.MemID });
        }

        // GET: Cash/Delete/5
        public ActionResult Delete(int? id,int memid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cash cash = db.Cash.Find(id);
            if (cash == null)
            {
                TempData["cash"] = "此種貨幣已歸零";
                return RedirectToAction("Display", "MemberCash", new { id = memid });
            }
            db.Cash.Remove(cash);
            db.SaveChanges();
            return RedirectToAction("Display", "MemberCash", new { id = memid });
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
