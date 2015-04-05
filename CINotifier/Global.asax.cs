using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using CINotifier.Logic.Infrastructure.Json;
using Newtonsoft.Json;

namespace CINotifier
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var serializerSettings = GlobalConfiguration
                 .Configuration
                 .Formatters.JsonFormatter
                 .SerializerSettings;
            serializerSettings.ContractResolver = new InternalContractResolver();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(ApiRouteConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}