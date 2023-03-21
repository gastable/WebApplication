using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMWP.Models;
using AMWP.ViewModels;
using Microsoft.Ajax.Utilities;

namespace AMWP.Controllers
{
    [LoginCheck(type = 2)]
    public class MembersController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        SetData sd = new SetData();

        [LoginCheck]
        // GET: Members
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.Currencies);
            return View(members.ToList());
        }
        [LoginCheck]
        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        [LoginCheck(flag =false)]
        // GET: Members/Create
        public ActionResult Create()
        {
            //ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name");
            ViewBag.CCYID = db.Currencies.ToList();
            return View();
        }

        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [LoginCheck(flag = false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemID,Account,Password,Name,CreatedDate,Status,CCYID")] Members members)
        {
            Boolean acntExists = db.Members.ToList().Exists(m => m.Account.Equals(members.Account));
            if (acntExists)
            {
                ViewBag.SignUp = "已有會員使用本帳號，請更換帳號！";
                ViewBag.CCYID = db.Currencies.ToList();
                return View(members);
            }
            if (ModelState.IsValid)
            {
                db.Members.Add(members);
                db.SaveChanges();
                TempData["SignUp"] = "註冊成功，請依您的帳號密碼登入本站！";
                return RedirectToAction("Login","MemberLogin");
            }
                ViewBag.CCYID = db.Currencies.ToList();
                return View(members);  
            
        }
        [LoginCheck]
        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", members.CCYID);
            return View(members);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [LoginCheck]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemID,Account,Password,Name,CreatedDate,Status,CCYID")] Members members)
        {
            if (ModelState.IsValid)
            {
                db.Entry(members).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", members.CCYID);
            return RedirectToAction("Index");
        }

        public ActionResult MemberEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", members.CCYID);
            return View(members);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberEdit([Bind(Include = "MemID,Account,Password,Name,CreatedDate,Status,CCYID")] Members members)
        {
            if (ModelState.IsValid)
            {
                db.Entry(members).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", members.CCYID);
            return View(members);
        }
        [LoginCheck]
        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            db.Members.Remove(members);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [LoginCheck]
        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var member = db.Members.Find(id);
            if (member != null)
            {
                string sql = "";
                if (member.Status)
                {
                    sql = "update Members set [Status] = 'false' where MemID = @id";
                }
                else
                {
                    sql = "update Members set [Status] = 'true' where MemID = @id";
                }  
                List<SqlParameter> list = new List<SqlParameter> { new SqlParameter("id", id) };
                sd.executeSql(sql,list);
                TempData["Status"] = Convert.ToString(member.Account) + "會員狀態改變！";
            }
            else
            {
                return HttpNotFound();
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
