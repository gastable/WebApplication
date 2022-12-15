using _03Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace _03Model.Controllers
{
    public class DefaultController : Controller
    {
        //建立一個dbSutdentEntities物件,取名叫db
        dbSutdentEntities db = new dbSutdentEntities();

        public ActionResult Index()
        {
            //var r = from s in db.tStudent
            //        select s;

            var students = db.tStudent.ToList();//記得多寫ToList()，讀取資料庫的list

            return View(students);
        }


        //新增
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tStudent student)
        {
            //檢查PK是否重覆
            var id = student.fStuId;
            var result = db.tStudent.Find(id);
            if (result != null)//代表PK重覆
            {
                ViewBag.ErrMsg = "學號重覆！！";
                return View(student);
            }
                

            if (ModelState.IsValid)//驗證是否符合模型欄位的驗證要求，IsValid是布林值
            {
                db.tStudent.Add(student);//把表單值新增至模型
                db.SaveChanges();//比對model跟db裡的不同後，自動產生下列程式碼把資料寫入資料庫

                //Insert into tStudent values(tStudent.fStuId,tStudent.fName,tStudent.Email,tStudent.Score)
                return RedirectToAction("Index");//資料順利儲存後，導回Index(list)頁面
            }
            return View(student);//要記得放入tStudent，這樣才能保持原資料狀態
        }

        public ActionResult Delete(string id)
        {
            //delete from tStudent where fStuID=id

            var student = db.tStudent.Find(id);//Find()只能用在primary key
            db.tStudent.Remove(student);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
