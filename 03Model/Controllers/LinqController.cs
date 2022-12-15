using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _03Model.Models;

namespace _03Model.Controllers
{
    public class LinqController : Controller
    {
        //建立一個DB Context物件,取名叫db
        NorthwindEntities db = new NorthwindEntities();

        //查詢所有員工資料
        public string ShowEmployee()
        {
            //SQL
            //select * from 員工

            //Linq
            //var result = from e in db.員工
            //             select e;

            var result = db.員工;

            string show = "";

            foreach (var item in result)
            {
                show += "員工編號：" + item.員工編號 + ", ";
                show += "姓名：" + item.姓名 + ", ";
                show += "職稱：" + item.職稱 + "<hr>";
            }

            return show;
        }


        //查詢所有客戶資料
        public string ShowCustomer()
        {
            //SQL
            //select * from 客戶

            //var result = from c in db.客戶
            //             select c;

            var result = db.客戶;

            string show = "";
            //隨便讀出客戶的三個欄位
            foreach (var item in result)
            {
                show += "客戶編號：" + item.客戶編號 + ", ";
                show += "公司名稱：" + item.公司名稱 + ", ";
                show += "地址：" + item.地址 + "<hr>";
            }

            return show;
        }


        //用地址關鍵字查詢符合條件的客戶資料
        public string ShowCustomerByAddress(string keyword)
        {
            //SQL
            //select * from 客戶 where 地址 like '%中正%'

            //Linq
            //var result = from c in db.客戶
            //             where c.地址.Contains(keyword)
            //             select c;

            //Linq擴充方法
            var result = db.客戶.Where(c => c.地址.Contains(keyword));

            //select * from 客戶 where 地址 like '中正%'
            //var result = db.客戶.Where(c => c.地址.StartsWith(keyword));


            //select * from 客戶 where 地址 like '%中正'
            //var result = db.客戶.Where(c => c.地址.EndsWith(keyword));

            string show = "";

            foreach (var item in result)
            {
                show += "客戶編號：" + item.客戶編號 + ", ";
                show += "公司名稱：" + item.公司名稱 + ", ";
                show += "地址：" + item.地址 + "<hr>";
            }

            return show;

        }

        //查詢單價大於30的產品，並依單價做遞增排序，庫存量做遞減排序
        public string ShowProduct()
        {
            //SQL
            //select * from 產品 where 單價>30 order by 單價,庫存量 desc

            //Linq
            //var result = from p in db.產品資料
            //             where p.單價>30
            //             orderby p.單價, p.庫存量 descending
            //             select p;


            //Linq擴充方法
            var result = db.產品資料.Where(p => p.單價 > 30).OrderBy(p => p.單價).ThenByDescending(p => p.庫存量);
                       

            string show = "";
            foreach (var item in result)
            {
                show += "產品編號：" + item.產品編號 + ", ";
                show += "產品：" + item.產品 + ", ";
                show += "單價：" + item.單價 + ", ";
                show += "庫存量：" + item.庫存量 + "<hr>";
            }
            return show;

        }



        public string ShowProductInfo()
        {
            //SQL
            //select conut(*),sum(單價),avg(單價), max(單價),min(單價) from 產品

            //Linq擴充方法
            var result = db.產品資料;


            string show = "";
         
            show += "產品筆數：" + result.Count() + ", ";
            show += "產品總價：" + result.Sum(p=>p.單價) + ", ";
            show += "單價平均：" + result.Average(p => p.單價) + ", ";
            show += "最高單價：" + result.Max(p => p.單價) + ", ";
            show += "最低單價：" + result.Min(p => p.單價) + "<hr>";
       
            return show;

        }

    }
}