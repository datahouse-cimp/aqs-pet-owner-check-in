using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using AQSOwnerCheckIn.Extensions;
using AQSOwnerCheckIn.Models;
using log4net;
using log4net.Config;
using Newtonsoft.Json.Serialization;

namespace AQSOwnerCheckIn
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WebApiApplication));

        static WebApiApplication()
        {
            XmlConfigurator.Configure();
        }
        protected void Application_Start()
        {
            Logger.Info("Application was initiated.");
            AreaRegistration.RegisterAllAreas();

            var serializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            var contractResolver = (DefaultContractResolver)serializerSettings.ContractResolver;
            contractResolver.IgnoreSerializableAttribute = true;

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            InforConfig.Register();
            InforConfig.LoadConfigs();
        }

        protected void Application_PostAuthorizeRequest()
        {
            // Read the SessionStateBehavior stored in the route's RouteData dictionary and set the session behavior in the context accordingly.
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = RouteTable.Routes.GetRouteData(context);

            if (routeData != null)
            {
                var sessionBehavior = routeData.GetSessionStateBehavior();
                context.SetSessionStateBehavior(sessionBehavior);
            }
        }
    }
}