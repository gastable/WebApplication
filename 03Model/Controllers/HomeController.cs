using _03Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _03Model.Controllers
{
    public class HomeController : Controller
    {
      
        public string  ShowAryDesc()
        {
            int[] score = { 78, 90, 20, 100, 66, 77 };

            string show = "";

            //Linq查詢運算式寫法
            //var result = from s in score
            //             orderby s descending
            //             select s;

            //Linq擴充方法的寫法
            var result = score.OrderByDescending(s => s);

            //var r = "123";

            //select s
            //from score
            //order by s desc
            show += "排序前:";
            foreach (var item in score)
            {
                show += item + ", ";
            }
            show += "<br>排序後:";
            foreach (var item in result)
            {
                show +=  + item + ", ";
            }


            return show;
        }


        public string ShowAryAsc()
        {
            int[] score = { 78, 90, 20, 100, 66, 77 };

            string show = "";

            //Linq查詢運算式寫法
            //var result = from s in score
            //             orderby s ascending
            //             select s;

            //Linq擴充方法的寫法
            var result = score.OrderBy(s => s);

            //var r = "123";

            //select s
            //from score
            //order by s

            show += "<br>排序後:";
            foreach (var item in result)
            {
                show += +item + ", ";
            }
            show += "<hr>";

            //select sum(s)
            //from score
            show += "成績總分:" + result.Sum();
            show += "<br>";
            show += "成績平均:" + result.Average();

            return show;
        }


        public string LoginMember(string uid, string pwd)
        {
            //Member member = new Member();   //鑄造物件
            //member.UId = "tom";
            //member.Pwd = "123";
            //member.Name = "湯姆";


            //Member member2 = new Member();
            //member2.UId = "jsper";
            //member2.Pwd = "456";
            //member2.Name = "賈斯伯";


            //Member member3 = new Member();
            //member3.UId = "mary";
            //member3.Pwd = "789";
            //member3.Name = "瑪麗";

            //string[] a;
            Member[] members = new Member[]
            {
                new Member{UId = "tom", Pwd = "123",Name = "湯姆"},
                new Member{UId = "jsper", Pwd = "456",Name = "賈斯伯"},
                new Member{UId = "mary", Pwd = "789",Name = "瑪麗"}
            };

            //SQL
            // select * from members where UId=uid and Pwd=pwd

            //Linq查詢運算式寫法
            //var result = (from m in members
            //              where m.UId == uid && m.Pwd == pwd
            //              select m).FirstOrDefault();

            //Linq擴充方法的寫法
            var result = members.Where(m => m.UId == uid && m.Pwd == pwd).FirstOrDefault();

            string show = "";

            if (result == null)
                show = "帳號或密碼錯誤";
            else 
                show = "登入成功！歡迎"+result.Name+"進入本系統";
            return show;
        }

    }
}