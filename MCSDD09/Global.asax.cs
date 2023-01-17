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
        //�{���i�J�I
        protected void Application_Start()
        {
            //�Ұ�db Initializer�إ߸�Ʈw
            //�p�G���ݭn�C�����m��Ʈw�A�i�H����o�q���ѱ�
            Database.SetInitializer<MCSDD09Context>(new MCSDD09Initializer());

            //���ε{���Ұʮɰ��檺�{���X
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
