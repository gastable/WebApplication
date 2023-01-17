using MCSDD09.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MCSDD09
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //程式進入點
        protected void Application_Start()
        {
            //啟動db Initializer建立資料庫
            //如果不需要每次重置資料庫，可以先把這段註解掉
            Database.SetInitializer<MCSDD09Context>(new MCSDD09Initializer());

            //應用程式啟動時執行的程式碼
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
