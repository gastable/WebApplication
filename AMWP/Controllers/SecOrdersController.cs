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
    [LoginCheck(type = 2)]
    public class SecOrdersController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        [LoginCheck]
        // GET: SecOrders
        public ActionResult Index()
        {
            var secOrders = db.SecOrders.Include(s => s.Members).Include(s => s.Securities);
            return View(secOrders.ToList());
        }

        [LoginCheck]
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
        public ActionResult _Create(int memId)
        {
            ViewBag.MemID = memId;
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol");
            return PartialView();
        }

        // POST: SecOrders/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "SSN,MemID,SecID,TrancType,Date,Share,Price,Fee")] SecOrders secOrders)
        {
            if (ModelState.IsValid)
            {
                db.SecOrders.Add(secOrders);
                db.SaveChanges();
                return RedirectToAction("DisplaySecOrders", "MemberSecurities", new {id=secOrders.MemID});
            }

            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", secOrders.MemID);
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", secOrders.SecID);
            return PartialView(secOrders);
        }

        // GET: SecOrders/Edit/5
        public ActionResult _Edit(long? id)
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
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", secOrders.SecID);
            return PartialView(secOrders);
        }

        // POST: SecOrders/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit([Bind(Include = "SSN,MemID,SecID,TrancType,Date,Share,Price,Fee")] SecOrders secOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DisplaySecOrders","MemberSecurities", new { id = secOrders.MemID });
            }
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", secOrders.MemID);
            ViewBag.SecID = new SelectList(db.Securities, "SecID", "Symbol", secOrders.SecID);
            return PartialView(secOrders);
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
            db.SecOrders.Remove(secOrders);
            db.SaveChanges();
            return RedirectToAction("DisplaySecOrders", "MemberSecurities", new { id = secOrders.MemID });
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
