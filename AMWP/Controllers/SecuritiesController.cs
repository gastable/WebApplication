using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMWP.Models;

namespace AMWP.Controllers
{
    public class SecuritiesController : Controller
    {
        private AMWPEntities db = new AMWPEntities();
        GetData gd = new GetData();

        // GET: Securities
        public ActionResult Index()
        {
            var securities = db.Securities.Include(s => s.Countries).Include(s => s.Currencies).Include(s => s.SecTypes);
            return View(securities.ToList());
        }

        // GET: Securities/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Securities securities = db.Securities.Find(id);
            if (securities == null)
            {
                return HttpNotFound();
            }
            return View(securities);
        }

        // GET: Securities/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name");
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name");
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name");
            return View();
        }

        // POST: Securities/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SecID,TypeID,CountryID,Symbol,Name,CCYID")] Securities securities)
        {
            var sec = db.Securities.Find(securities.SecID);
            if (sec != null)
            {
                ViewBag.PKCheck = "證券代碼重覆";
            }
            else if (ModelState.IsValid)
            {
                db.Securities.Add(securities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", securities.CountryID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", securities.CCYID);
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name", securities.TypeID);
            return View(securities);
        }

        // GET: Securities/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Securities securities = db.Securities.Find(id);
            if (securities == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", securities.CountryID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", securities.CCYID);
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name", securities.TypeID);
            return View(securities);
        }

        // POST: Securities/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecID,TypeID,CountryID,Symbol,Name,CCYID")] Securities securities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(securities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "Name", securities.CountryID);
            ViewBag.CCYID = new SelectList(db.Currencies, "CCYID", "Name", securities.CCYID);
            ViewBag.TypeID = new SelectList(db.SecTypes, "TypeID", "Name", securities.TypeID);
            return View(securities);
        }

        // GET: Securities/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Securities securities = db.Securities.Find(id);
            if (securities == null)
            {
                return HttpNotFound();
            }
            try
            {
                db.Securities.Remove(securities);
                db.SaveChanges();
            }
            catch
            {
                TempData["ForeignKey"] = "已有會員交易此證券，無法刪除！";
            }
            return RedirectToAction("Index");
        }

            [HttpPost]
        public ActionResult Upload(HttpPostedFileBase postedFile)
        {            
                string filePath = string.Empty;
                if (postedFile != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    //Create a DataTable.
                    DataTable dt = new DataTable();
                    //dt.Columns.Add("SecID",typeof(string)).DefaultValue = "B00001";
                    dt.Columns.AddRange(new DataColumn[9] {                                 
                                new DataColumn("Date", typeof(DateTime)),
                                new DataColumn("Open",typeof(double)),
                                new DataColumn("High",typeof(double)),
                                new DataColumn("Low",typeof(double)),
                                new DataColumn("Close",typeof(double)),
                                new DataColumn("AdjClose",typeof(double)),
                                new DataColumn("Volume",typeof(long)),
                                new DataColumn("Dividend",typeof(double)),
                                new DataColumn("SplitCoefficient",typeof(double))});
                    DataColumn dc = new DataColumn("SecID", typeof(string));
                    dc.DefaultValue = "A00002";
                    dt.Columns.Add(dc);

                    //Read the contents of CSV file.
                    string csvData = System.IO.File.ReadAllText(filePath);

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dt.Rows.Add();
                            int i = 0;

                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                    }

                    string conString = ConfigurationManager.ConnectionStrings["AMWPConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.Daily";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("SecID", "SecID");
                            sqlBulkCopy.ColumnMappings.Add("Date", "Date");
                            sqlBulkCopy.ColumnMappings.Add("Open", "Open"); 
                            sqlBulkCopy.ColumnMappings.Add("High", "High");
                            sqlBulkCopy.ColumnMappings.Add("Low", "Low"); 
                            sqlBulkCopy.ColumnMappings.Add("Close", "Close");
                            sqlBulkCopy.ColumnMappings.Add("AdjClose", "AdjClose");
                            sqlBulkCopy.ColumnMappings.Add("Volume", "Volume");
                            sqlBulkCopy.ColumnMappings.Add("Dividend", "Dividend");

                        con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
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
