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
    [LoginCheck]
    public class ManagementsController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: Managements
        public ActionResult Index()
        {
            var managements = db.Managements.Include(m => m.Admins).Include(m => m.Members);
            return View(managements.ToList());
        }

        // GET: Managements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Managements managements = db.Managements.Find(id);
            if (managements == null)
            {
                return HttpNotFound();
            }
            return PartialView(managements);
        }

        // GET: Managements/Create
        public ActionResult Create()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account");
            return View();
        }

        // POST: Managements/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,MemID,AdminID,Action,Reason,CreatedDate")] Managements managements)
        {
            if (ModelState.IsValid)
            {
                db.Managements.Add(managements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", managements.AdminID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", managements.MemID);
            return View(managements);
        }

        // GET: Managements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Managements managements = db.Managements.Find(id);
            if (managements == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", managements.AdminID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", managements.MemID);
            return View(managements);
        }

        // POST: Managements/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,MemID,AdminID,Action,Reason,CreatedDate")] Managements managements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", managements.AdminID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", managements.MemID);
            return View(managements);
        }

        // GET: Managements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Managements managements = db.Managements.Find(id);
            if (managements == null)
            {
                return HttpNotFound();
            }
            return View(managements);
        }

        // POST: Managements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Managements managements = db.Managements.Find(id);
            db.Managements.Remove(managements);
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
