using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01Controller.Controllers
{
    public class FileUpLoadController : Controller
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
            if (photo != null)
            {
                if (photo.ContentLength > 0)
                {
                    if (photo.ContentLength <= 5242880)
                    {

                        index = photo.FileName.IndexOf(".");  //抓字串.的索引位置
                        subName = photo.FileName.Substring(index+1, 3);  //抓副檔名
                        subName = subName.ToLower();

                        if (subName == "jpg" || subName == "png")
                        {
                            photo.SaveAs(Server.MapPath("~/photos/" + photo.FileName));
                            //D:\WebApplication\01Controller\photos
                            ViewBag.Message = "檔案上傳成功!!";
                        }
                        else
                        {
                            ViewBag.Message = "檔案格式有問題!!";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "檔案大於5M!!";
                    }
                }
                else
                {
                    ViewBag.Message = "您傳的一個空檔案!!";
                }
            }
            else
            {
                ViewBag.Message = "您沒有上傳任何檔案!!";
            }

            return View();
        }


        //寫一個顯示photos資料夾中所有照片的功能(一般函數)
        public string ShowPhotos()
        {
            string show = "";
            DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/photos/"));
            FileInfo[] files= d.GetFiles();

            foreach(FileInfo item in files)
            {
                show += "<img src='../photos/"+item.Name+"' />";
            }
            show += "<hr><a href='/FileUpLoad/Create'>點我回上傳檔案頁面</a>";
            return show;
        }

    }
}