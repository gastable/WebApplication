using MCSDD10.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MCSDD10
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //�Ұ�DB Initializer�إ߸�Ʈw�A���\�إ߫�n���ѱ��A�p�G�H��n���ئA���}
            //Database.SetInitializer<MCSDD10Context>(new MCSDD10Initializer());  //SetInitializer��k���ѼƬOMCSDD10Initializer()����

            //���ε{���Ұʮɰ��檺�{���X
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
