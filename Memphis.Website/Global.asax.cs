using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Memphis.Website.App_Start;
using Memphis.Website.Mapping;

namespace Memphis.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.RegisterAutoMaps();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
