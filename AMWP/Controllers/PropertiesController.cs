﻿using System;
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
    public class PropertiesController : Controller
    {
        private AMWPEntities db = new AMWPEntities();

        // GET: Properties
        public ActionResult Index()
        {
            var properties = db.Properties.Include(p => p.Currencies).Include(p => p.Members);
            return View(properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Properties properties = db.Properties.Find(id);
            if (properties == null)
            {
                return HttpNotFound();
            }
            return View(properties);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name");
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account");
            return View();
        }

        // POST: Properties/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SSN,MemID,Name,Price,Date,Loan,Term,Principal,PayDay,CCYID")] Properties properties)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(properties);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", properties.CCYID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", properties.MemID);
            return View(properties);
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Properties properties = db.Properties.Find(id);
            if (properties == null)
            {
                return HttpNotFound();
            }
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", properties.CCYID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", properties.MemID);
            return View(properties);
        }

        // POST: Properties/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SSN,MemID,Name,Price,Date,Loan,Term,Principal,PayDay,CCYID")] Properties properties)
        {
            if (ModelState.IsValid)
            {
                db.Entry(properties).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", properties.CCYID);
            ViewBag.MemID = new SelectList(db.Members, "MemID", "Account", properties.MemID);
            return View(properties);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Properties properties = db.Properties.Find(id);
            if (properties == null)
            {
                return HttpNotFound();
            }
            return View(properties);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Properties properties = db.Properties.Find(id);
            db.Properties.Remove(properties);
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