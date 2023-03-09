using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCSDD10.Models;
using PagedList;  //記得using PagedList套件

namespace MCSDD10.Controllers
{
    //[LoginCheck]
    public class MembersController : Controller
    {
        private MCSDD10Context db = new MCSDD10Context();
        SetData sd = new SetData();

        int pageSize = 10; //每頁幾筆資料
        // GET: Members
        public ActionResult Index(int page=1)
        {
            int currentPage = page<1?1:page;  //現在第幾頁，而且擋使用者輸入0以下的頁數
            var member = db.Members.ToList();
            var result = member.ToPagedList(currentPage,pageSize); //PagedList的擴充方法(要呈現頁次,每頁幾筆)

            return View(result);
        }

        //[ChildActionOnly]
        // GET: Members/Details/5
        public ActionResult _Details(int? id)
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
            return PartialView(members);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,MemberName,Account,CreatedDate,MemberBirthday,MemberPhotoFile,Password")] Members members)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(members);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(members);
        }

        // GET: Members/Edit/5
        public ActionResult _Edit(int? id)
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
            return PartialView(members);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit( Members members)
        {
            string sql = "update members set MemberName=@MemberName, MemberBirthday=@MemberBirthday where MemberID=@MemberID";

            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter("MemberName",members.MemberName),
                new SqlParameter("MemberBirthday",members.MemberBirthday),
                new SqlParameter("MemberID",members.MemberID)
            };
            try 
            { 
                sd.executeSql(sql, list);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ExcptMsg=ex.Message;
                return PartialView(members);
            }
           
        }

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
            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Members members = db.Members.Find(id);
            db.Members.Remove(members);
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
