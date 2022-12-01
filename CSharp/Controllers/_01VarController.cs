using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharp.Controllers
{
    public class _01VarController : Controller
    {
        public string MyFirstCS()
        {
            string w = "Hello World";


            return w;
        }

        public int MyFirstCS2()
        {
            int w = 5745457;  //32位元帶正負號整數


            return w;
        }

        public string checkGender(bool gender)
        {
            bool w = gender;

            if (w)
                return "男生";
            
           
            return "女生";
        }


        public string checkGender2(string name, bool gender)
        {
            bool w = gender;
            string n = name;

            string r = "";

            r = w ? "男生" : "女生";

           

            return "<h1>"+n+"您好!您的性別是"+r+"</h1>";
        }

        public void number()
        {
            //數值Data Type
           
            byte a = 123; //8位元正整數(0~255)
            sbyte b = 123; //8位元整數(-128~127)

            short c = 123;  //16位元整數(-32768~32767)
            int d = 123;  //32位元整數(-2147483648~2147483647)
            long e = 123; //64位元整數

            ushort f = 123; //16位元正整數(0~65535)
            uint g = 123;  //32位元正整數(0~4294967295)
            ulong h = 123; //64位元正整數

            /////////////////////////
            float i = 123.345f; //單精準度浮點數
            double j = 123.34345435; //倍(雙)精準度浮點數


        }

        public string getGoogleMap(string place)
        {
            return "<a href='https://www.google.com.tw/maps/search/" + place + "'>點我看地圖</a>";
          
             
        }



    }
}