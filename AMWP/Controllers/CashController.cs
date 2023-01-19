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
    public class CashController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: Cash
        public ActionResult Index()
        {
            var cash = db.Cash.Include(c => c.Currencies).Include(c => c.Members);
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
        public ActionResult Create()
        {
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name");
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account");
            return View();
        }

        // POST: Cash/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,MemID,CCYID,Amount")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                db.Cash.Add(cash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", cash.CCYID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", cash.MemID);
            return View(cash);
        }

        // GET: Cash/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", cash.CCYID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", cash.MemID);
            return View(cash);
        }

        // POST: Cash/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,MemID,CCYID,Amount")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", cash.CCYID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", cash.MemID);
            return View(cash);
        }

        // GET: Cash/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Cash/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cash cash = db.Cash.Find(id);
            db.Cash.Remove(cash);
            db.SaveChanges();
            return RedirectToAction("Index");
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
