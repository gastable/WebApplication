using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Xml.Linq;

namespace CSharp.Controllers
{
    public class _02StatementController : Controller
    {
        //運算式
        public void Statement()
        {
            int a = 10;
            a = 20;

            a = a + 10; //a目前為30
            a += 10;  //a目前為40
            a -= 15; //a目前為25
            a *= 10; //a目前為250
            a /= 25; //a目前為10

            a = a + 1;
            a += 1;
            a++; //a目前為13

            Response.Write(a);
            Response.Write("<hr>");
            //////////////////////////////
            ///
            int x = 123;
            float y = 12.1234567f;
            float z = 12.134f;
            float result = 0;

            result = x + z;
            Response.Write("y=" + y);
            Response.Write("<br>");
            Response.Write("result=" + result);
            Response.Write("<hr>");


            float xx = 10000.9f;
            float yy = 9999.3f;
            Response.Write("xx-yy=" + (xx-yy));

            Response.Write("<hr>");
       
            Response.Write("xx-yy=" + ((decimal)xx - (decimal)yy));

        }

        //if敘述
        public string IfStatement(int score)
        {
            //int score = 95;

            if (score >= 90)
               return "優等";
            else if (score >= 80)
                return "甲等";
            else if (score >= 70)
                return "乙等";
            else if (score >= 60)
                return "丙等";

            return "丁等";
        }


        //Switch敘述
        public void SwitchStatement(string strColor)
        {

            switch (strColor)
            {
                case "y":
                    Response.Write("黃色");
                    break;
                case "g":
                    Response.Write("綠色");
                    break;
                case "r":
                    Response.Write("紅色");
                    break;
                default:
                    Response.Write("這不是黃綠紅");
                    break;
              
            }
        }

        //For敘述
        public string ForStatement()
        {
            string[] Rainbow = { "紅", "橙", "黃", "綠", "藍", "靛", "紫" };  //陣列
            string[] RainbowColor = { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };

            string result = "";

            for(int i = 0; i < Rainbow.Length; i++)
            {
                result += "<h2 style='color:" + RainbowColor[i] + "'>" + Rainbow[i] + "</h2>";
            }

            return result;

        }

        //Foreach敘述
        public void ForeachStatement()
        {
            string[] Rainbow = { "紅", "橙", "黃", "綠", "藍", "靛", "紫" };  //陣列

            foreach (string item in Rainbow)
            {
                Response.Write("<h2>" + item + "</h2>");
            }

        }
    }
}


//算數運算與指定運算
//1.算數運算子 +,-,*,/,%
//2.（）小括號
//3.連接符號 +
//4.負號 -
//5.優先權
//  ()
//  * , / , %
//  + , -
//6.留意浮點數運算後遺失精準度的問題

//選擇判斷式
//1.if
//2.switch

//迴圈
//1.for
//2.foreach
//3.while
//4.do....while