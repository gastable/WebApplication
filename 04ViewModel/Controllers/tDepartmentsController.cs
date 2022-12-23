using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _04ViewModel.Models;

namespace _04ViewModel.Controllers
{
    public class tDepartmentsController : Controller
    {
        private dbEmployeeEntities db = new dbEmployeeEntities();

        // GET: tDepartments
        public ActionResult Index()
        {
            return View(db.tDepartment.ToList());
        }

        // GET: tDepartments/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tDepartment tDepartment = db.tDepartment.Find(id);
        //    if (tDepartment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tDepartment);
        //}

        // GET: tDepartments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tDepartments/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fDepId,fDepName")] tDepartment tDepartment)
        {
            if (ModelState.IsValid)
            {
                db.tDepartment.Add(tDepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tDepartment);
        }

        // GET: tDepartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tDepartment tDepartment = db.tDepartment.Find(id);
            if (tDepartment == null)
            {
                return HttpNotFound();
            }
            return View(tDepartment);
        }

        // POST: tDepartments/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fDepId,fDepName")] tDepartment tDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tDepartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tDepartment);
        }

        // GET: tDepartments/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tDepartment tDepartment = db.tDepartment.Find(id);
        //    if (tDepartment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tDepartment);
        //}

        // POST: tDepartments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            tDepartment tDepartment = db.tDepartment.Find(id);
            db.tDepartment.Remove(tDepartment);
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
