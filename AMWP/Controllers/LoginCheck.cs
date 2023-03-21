using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMWP.Controllers
{
    public class LoginCheck : ActionFilterAttribute
    {
        public bool flag = true;  //直接把flag欄位設定為true，不是用屬性set;get

        public short type = 1;
        

        //旗標控制
        void MemberLoginState(HttpContext context)   //沒有回傳值的函式，要用HttpContext當參數來判斷session
        {
            if (context.Session["mem"] == null)
            {
                context.Response.Redirect("/MemberLogin/Login");
            }
        }

        void AdminLoginState(HttpContext context)   //沒有回傳值的函式，要用HttpContext當參數來判斷session
        {
            if (context.Session["admin"] == null)
            {
                context.Response.Redirect("/AdminLogin/Login");
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)  //執行任何action時就執行以下動作
        {
            if (flag)
            {
                HttpContext context = HttpContext.Current;      //抓目前HttpContext的狀態，抽象類別不用new的

                if (type == 1)
                    AdminLoginState(context);
                else
                    MemberLoginState(context);
            }
        }
    }
}