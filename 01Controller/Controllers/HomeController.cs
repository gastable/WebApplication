//本區是Name Space(命名空間)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01Controller.Controllers
{
    public class HomeController : Controller//Controller就要寫在Controller類別裡面
    {
        // 修飾詞decorator
        //public(指大家[瀏覽器也行]都能存取)
        //protected(指我的家人才能存取，或跟我有關係的親戚)
        //private(指我的家人[程式]才能存取)

        //這種函數是Controller裡的專用函數，稱做Action，是ActionResult的資料型態
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public string ShowAry()
        {
            int[] score = { 55, 65, 75, 85, 95, 100, 45 };
            int sum = 0;
            string show = "";
            foreach (int s in score)
            {
                sum += s;//把陣列的值加總起來
                show += s + ",";//陣列讀出來，串成字串
            }
            show += "總成績為" + sum;
            return show;
        }




        public string ShowImages()
        {
            string show = "";
            for(int i = 1; i <=8; i++)
            {
                show += "<a href='ShowImagesIndex?index="+i+"'><img src='../images/" + i + ".jpg' width='300'/></a>";
            }
            return show;
        }
        public string ShowImagesIndex(int index)
        {
            string show = "";
            string show1 = "";
            string[] name = { "櫻桃鴨", "鴨油高麗菜", "鴨油麻婆豆腐", "櫻桃鴨握壽司", "片皮鴨捲三星蔥", "三杯鴨", "櫻桃鴨片肉", "慢火白菜燉鴨湯" };
            show = "<p style='text-align:center'><img src='../images/" + index + ".jpg'><h1 style='text-align:center'>" + name[index-1]+"</h1></p><div style='text-align:center'><a href='ShowImages'>回上頁</a></div><hr>";
            show1 = string.Format("<p style='text-align:center'><img src='../images/{0}.jpg'><h1 style='text-align:center'>{1}</h1></p><div style='text-align:center'><a href='ShowImages'>回上頁</a></div><hr>", index, name[index - 1]);
            return show+show1;
        }

    }
}