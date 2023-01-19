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
    public class SecOrdersController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: SecOrders
        public ActionResult Index()
        {
            var secOrders = db.SecOrders.Include(s => s.Members).Include(s => s.Securities);
            return View(secOrders.ToList());
        }

        // GET: SecOrders/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecOrders secOrders = db.SecOrders.Find(id);
            if (secOrders == null)
            {
                return HttpNotFound();
            }
            return View(secOrders);
        }

        // GET: SecOrders/Create
        public ActionResult Create()
        {
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account");
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol");
            return View();
        }

        // POST: SecOrders/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,MemID,SecID,TrancType,Date,Share,Price,Fee")] SecOrders secOrders)
        {
            if (ModelState.IsValid)
            {
                db.SecOrders.Add(secOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", secOrders.MemID);
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", secOrders.SecID);
            return View(secOrders);
        }

        // GET: SecOrders/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecOrders secOrders = db.SecOrders.Find(id);
            if (secOrders == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", secOrders.MemID);
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "TypeID", secOrders.SecID);
            return View(secOrders);
        }

        // POST: SecOrders/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,MemID,SecID,TrancType,Date,Share,Price,Fee")] SecOrders secOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", secOrders.MemID);
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "TypeID", secOrders.SecID);
            return View(secOrders);
        }

        // GET: SecOrders/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecOrders secOrders = db.SecOrders.Find(id);
            if (secOrders == null)
            {
                return HttpNotFound();
            }
            return View(secOrders);
        }

        // POST: SecOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SecOrders secOrders = db.SecOrders.Find(id);
            db.SecOrders.Remove(secOrders);
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
