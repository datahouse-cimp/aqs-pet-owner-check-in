using System.Web.Http;
using System.Web.SessionState;
using AQSOwnerCheckIn.Extensions;
using AQSOwnerCheckIn.Models;
using log4net;

namespace AQSOwnerCheckIn
{
    public static class WebApiConfig
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WebApiConfig));
        
        public static void Register(HttpConfiguration config)
        {
            Logger.Info("Method called.");
            // Use this section to describe API routes
            // config.Routes.MapHttpRoute(
            //     name: "[Unique name]",
            //     routeTemplate: "[Route address. Relative to the site root e.g. http://localhost/auth/v1/login] auth/v1/login",
            //     defaults: new { controller = "[Name of controller minus the word "Controller"] AngularConfiguration", 
            //        action = "[Name of action inside controller if you want to get more specific]" }
            // );
            // Note: Adding {someWord} to the end of a routeTemplate will add a route parameter requirement to the route.
            //       Specifying an action will map a single routeTemplate to the action name within the specified controller.
            //       Not specifying an action will make the configuration assume that there will be a single route matching the route verbs (e.g. GET, POST, PUT, etc) for the specified controller
            //          In such cases, the methods must match these names as Get, Post, Put respectively. 
            
            config.Routes.MapHttpRoute(
                name: "Login",
                routeTemplate: "auth/v1/login",
                defaults: new { controller = "Authentication", action = "Login" },
                sessionBehavior: SessionStateBehavior.Required
            );

            config.Routes.MapHttpRoute(
                name: "Logout",
                routeTemplate: "auth/v1/logout",
                defaults: new { controller = "Authentication", action = "Logout" },
                sessionBehavior: SessionStateBehavior.Required
            );

            config.Routes.MapHttpRoute(
                name: "LoggedInUser",
                routeTemplate: "auth/v1/logged-in-user",
                defaults: new { controller = "Authentication", action = "LoggedInUser" },
                sessionBehavior: SessionStateBehavior.ReadOnly
            );

            config.Routes.MapHttpRoute(
                name: "SampleRoute",
                routeTemplate: "api/v1/contact-search",
                defaults: new { controller = "Sample", action = "ContactSearch" },
                sessionBehavior: SessionStateBehavior.ReadOnly
            );

           
            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
