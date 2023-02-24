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
    public class AdminsController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: Admins
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminID,Account,Password,Name")] Admins admins)
        {
            var adm = db.Admins.Find(admins.AdminID);
            if (adm != null)
            {
                ViewBag.PKCheck = "管理員代碼重覆";
            }
            else if(ModelState.IsValid)
            {
                db.Admins.Add(admins);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admins);
        }

        // GET: Admins/Edit/5
        public ActionResult _Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admins admins = db.Admins.Find(id);
            if (admins == null)
            {
                return HttpNotFound();
            }
            return PartialView(admins);
        }

        // POST: Admins/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit([Bind(Include = "AdminID,Account,Password,Name")] Admins admins)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admins).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            return PartialView(admins);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admins admins = db.Admins.Find(id);
            if (admins == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.Admins.Remove(admins);
                db.SaveChanges();
            }
            catch
            {
                TempData["ForeignKey"] = "該管理員已有處理紀錄，無法刪除！";
            }
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
