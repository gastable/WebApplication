using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharp.Controllers
{
    public class MyPracticeController : Controller
    {
        //If敘述
        public string IfStatement(int score)
        {
            if (score >= 90)
                return "優等";
            else if (score >= 80)
                return "甲等";
            else if (score >= 70)
                return "乙等";
            else if (score >= 60)
                return "丙等";
            else
                return "丁等";
        }
        //Switch敘述
        public string SwitchStatement(string strColor)
        {
            switch (strColor)
            {
                case "r":
                    return "紅色";
                case "g":
                    return "綠色";
                case "b":
                    return "藍色";
                default:
                    return "這不是三原色";
            }
        }

        //For敘述
        public string ForStatement()
        {
            string[] Rainbow = { "紅", "橙", "黃", "綠", "藍", "靛", "紫" };//陣列
            string[] RainbowColor = { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
            string result = "";
            for (int i = 0; i < Rainbow.Length; i++)
            {
                result += "<h1 style='color:" + RainbowColor[i] + "'>" + Rainbow[i] + "</h1>";
            }
            return result;
        }
        //Foreach敘述
        public void ForeachStatement()
        {
            string[] Rainbow = { "紅", "橙", "黃", "綠", "藍", "靛", "紫" };
            foreach (string item in Rainbow)
            {
                Response.Write("<h1>" + item + "</h1>");
            }
        }
        //第一題,判斷質數
        public void No1(int n)
        {
            if (n == 0 || n == 1)
            {
                Response.Write(n + "不是質數");
            }
            else
            {
                int c = 0;
                for (int i = 1; i <= n; i++)
                {
                    if (n % i == 0)
                        c++;
                }
                if (c > 2)
                    Response.Write(n + "不是質數");
                else Response.Write(n + "是質數");
            }
        }
        //第二題,找任意兩數之最大公因數
        public void No2(int a, int b)
        {
            int x = a;
            int y = b;
            var c = a % b;
                while (c != 0)
                {
                    a = b;
                    b = c;
                    c = a % b;
                }
                Response.Write(x + "與" + y + "的最大公因數為" + b);            
        }
        //第三題,迴文判斷
        public string No3(int n)
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int d = n;
            while (n > 0)
            {
                a = n % 10;
                b = n / 10;
                c = c * 10 + a;
                n = b;
            }
            if (c == d)
                return d + "是迴文";
            return d + "不是迴文";
        }
        //第四題,發牌程式
        public void No4()
        {
            string[] poker = new string[52];
            for (int i = 0; i < poker.Length; i++)
            {
                poker[i] = (i + 1).ToString();
            }

            ////洗牌
            Random ran = new Random();
            string seat = "";
            int r = 0;
            for (int i = 0; i < poker.Length; i++)
            {
                r = ran.Next(0, poker.Length);
                seat = poker[i];
                poker[i] = poker[r];
                poker[r] = seat;
            }
            Response.Write("洗牌結果:<br>");
            for(int i = 0; i < poker.Length; i++)
            {
                Response.Write("<img src='../poker_img/" + poker[i] + ".gif' />");
            }

            Response.Write("<hr>");
            //發牌
            string player1 = "", player2 = "", player3 = "", player4 = "";
            string result = "";
            for (int i = 0; i < poker.Length; i++)
            {
                result= "<img src='../poker_img/" + poker[i] + ".gif' />";
                switch (i % 4)
                {
                    case 0:
                         player1 += result;
                        break;
                    case 1:
                        player2 += result; 
                        break;
                    case 2:
                        player3 += result;
                        break;
                    case 3:
                        player4 += result;
                        break;                        
                }
            }
            Response.Write("發牌結果:<br>");
            Response.Write("第一位玩家的牌:" + player1 + "<hr>");
            Response.Write("第二位玩家的牌:" + player2 + "<hr>");
            Response.Write("第三位玩家的牌:" + player3 + "<hr>");
            Response.Write("第四位玩家的牌:" + player4 + "<hr>");
        }
    }
}