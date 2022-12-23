using _04ViewModel.Models;
using _04ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _04ViewModel.Controllers
{
    public class tEmployeeController : Controller
    {
        //第1件事：先建立dbContext
        private dbEmployeeEntities db = new dbEmployeeEntities(); //private前綴，只存在tEmployeeController內，為預設可不寫

        //讀出tEmployee資料表，並建立List

        public ActionResult Index(int deptId=2)
        {
            //EmpDept emp = new EmpDept();舊寫法

            //emp.department=db.tDepartment.ToList();
            //emp.employee=db.tEmployee.Where(e=>e.fDepId==1).ToList();

            //key value語法
            EmpDept emp = new EmpDept() {

                department = db.tDepartment.ToList(),
                employee = db.tEmployee.Where(e => e.fDepId == deptId).ToList()
             };
            ViewBag.deptName=db.tDepartment.Find(deptId).fDepName;//
            ViewBag.deptId = deptId;//帶部門代碼去Index

            return View(emp);
        }

        public ActionResult Create()
        {
            ViewBag.Dept=db.tDepartment.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tEmployee employee)
        {
            var emp = db.tEmployee.Find(employee.fEmpId);
            if (emp!=null)
            {
                ViewBag.PKCheck = "員工代號重覆";
            }
            else if(ModelState.IsValid)   //如果Model沒有寫驗證器，所以要try catch避免例外時，不會跑出系統錯誤訊息
            {
                db.tEmployee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index", new {deptId=employee.fDepId});
            }
            else 
            { 
                ViewBag.Msg = "驗證未通過，請檢查表單資料是否填寫正確";
            }
                ViewBag.Dept = db.tDepartment.ToList();
                return View(employee);                           
        }

        public ActionResult Edit(string id)
        {
            var emp = db.tEmployee.Find(id);

            ViewBag.Dept = db.tDepartment.ToList();

            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tEmployee employee)
        {
            if (ModelState.IsValid)   //如果Model沒有寫驗證器，所以要try catch避免例外時，不會跑出系統錯誤訊息
            {
                //把dbContext設定為可被修改的狀態
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { deptId = employee.fDepId });
            };
            ViewBag.Msg = "驗證未通過，請檢查輸入資料是否正確";
            ViewBag.Dept = db.tDepartment.ToList();
            return View(employee);
        }

        public ActionResult Delete(string id)
        {
            var emp = db.tEmployee.Find(id);
            db.tEmployee.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index", new { deptId = emp.fDepId });
        }             
    }
}