using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace MCSDD10.Controllers
{
    public class LoginCheck: ActionFilterAttribute  //繼承System.Web.Mvc的，不是System.Web.Http.Filters的
    {
        void LoginState(HttpContext context)   //沒有回傳值的函式，要用HttpContext當參數來判斷session
        {
            if (context.Session["user"] == null) 
            {
                context.Response.Redirect("/HomeManager/Login");
            };
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)  //執行任何action時就執行以下動作
        {
            HttpContext context = HttpContext.Current;      //抓目前HttpContext的狀態，抽象類別不用new的
            LoginState(context);
        }
    }
}