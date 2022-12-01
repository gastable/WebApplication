using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace _01Controller.Controllers
{
    public class MyPracticeController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase photo)
        {
            int index = 0;
            string subName = "";
            if(photo != null) {
                if (photo.ContentLength > 0) {
                    if (photo.ContentLength<1048576) {
                        index = photo.FileName.IndexOf(".");
                        subName=photo.FileName.Substring(index+1, 3);
                        subName = subName.ToLower();
                        if (subName=="jpg"|| subName =="png") { 

            photo.SaveAs(Server.MapPath("~/photos/"+photo.FileName));

                        ViewBag.message = "檔案上傳成功";
                        }
                        else
                        {
                            ViewBag.message = "檔案格式錯誤";

                        }
                    }
                    else
                    {
                        ViewBag.message = "檔案大於1mb";

                    }
                }
                else
                {
                    ViewBag.message = "您傳了一個空檔案";

                }
            }
            else
            {
                ViewBag.message = "您未上傳任何檔案";
            }
            return View();
        }

        public string showUpdate()
        {
            string show = "";
            DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/photos"));
            FileInfo[] files = d.GetFiles();
            foreach(FileInfo item in files)
            {
                show += "<img src='../photos/"+item.Name+"'>";
            }
            show += "<hr/><a href='/FileUpLoad/Create'>點我回「上傳照片」</a>";
            return show;
        }
    }
}