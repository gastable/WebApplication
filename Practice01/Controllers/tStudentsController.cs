using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practice01.Models;

namespace Practice01.Controllers
{
    public class tStudentsController : Controller
    {
        private dbSutdentEntities db = new dbSutdentEntities();

        // GET: tStudents
        public ActionResult Index()
        {
            return View(db.tStudent.ToList());
        }

        // GET: tStudents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tStudent tStudent = db.tStudent.Find(id);
            if (tStudent == null)
            {
                return HttpNotFound();
            }
            return View(tStudent);
        }

        // GET: tStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tStudents/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fStuId,fName,fEmail,fScore")] tStudent tStudent)
        {
            if (ModelState.IsValid)
            {
                db.tStudent.Add(tStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tStudent);
        }

        // GET: tStudents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tStudent tStudent = db.tStudent.Find(id);
            if (tStudent == null)
            {
                return HttpNotFound();
            }
            return View(tStudent);
        }

        // POST: tStudents/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fStuId,fName,fEmail,fScore")] tStudent tStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tStudent);
        }

        // GET: tStudents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tStudent tStudent = db.tStudent.Find(id);
            if (tStudent == null)
            {
                return HttpNotFound();
            }
            return View(tStudent);
        }

        // POST: tStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tStudent tStudent = db.tStudent.Find(id);
            db.tStudent.Remove(tStudent);
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
