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
    public class SecTypesController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: SecTypes
        public ActionResult Index()
        {
            return View(db.SecTypes.ToList());
        }

        // GET: SecTypes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecTypes secTypes = db.SecTypes.Find(id);
            if (secTypes == null)
            {
                return HttpNotFound();
            }
            return View(secTypes);
        }

        // GET: SecTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecTypes/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeID,Name")] SecTypes secTypes)
        {
            if (ModelState.IsValid)
            {
                db.SecTypes.Add(secTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secTypes);
        }

        // GET: SecTypes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecTypes secTypes = db.SecTypes.Find(id);
            if (secTypes == null)
            {
                return HttpNotFound();
            }
            return View(secTypes);
        }

        // POST: SecTypes/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeID,Name")] SecTypes secTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secTypes);
        }

        // GET: SecTypes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecTypes secTypes = db.SecTypes.Find(id);
            if (secTypes == null)
            {
                return HttpNotFound();
            }
            return View(secTypes);
        }

        // POST: SecTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SecTypes secTypes = db.SecTypes.Find(id);
            db.SecTypes.Remove(secTypes);
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
