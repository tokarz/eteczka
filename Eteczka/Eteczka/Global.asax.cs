using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Eteczka
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            EteczkaConfig.InitSystem();

            IUnityContainer container = UnityConfig.GetConfiguredContainer();
            UnityConfig.RegisterStaticTypes(container);
            UnityConfig.RegisterTypes(container);
            UnityActivator.Start(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
