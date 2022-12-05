using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01Controller.Controllers
{
    public class MultiFileUploadController : Controller
    {
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase[] photos)
        {
           if(photos != null)
            {
                foreach(var photo in photos)
                {
                    if(photo.ContentLength>0)
                    {
                        photo.SaveAs(Server.MapPath("~/photos/"+photo.FileName));
                        ViewBag.Message = "上傳成功";
                    }
                   
                }
            }
            else
            {
                ViewBag.Message = "您沒有上傳任何檔案!!";
            }

            return View();
        }
    }
}