using System.Web.Mvc;
using System.Web.Routing;

namespace AQSOwnerCheckIn
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.AppendTrailingSlash = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Set the default to the relative location of the index.html file
            var route = routes.MapPageRoute("Main", "", "~/Public/index.html");

            var url = route.Url;
        }
    }
}