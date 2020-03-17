using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;
using System.Web.SessionState;
using AQSOwnerCheckIn.Models;

namespace AQSOwnerCheckIn.Extensions
{
    // Reference: https://stackoverflow.com/a/34727708
    // Helper class that extends the HttpRouteCollection to capture desired session behavior for a route described using MapHttpRoute.
    public static class MapHttpRouteExtension
    {
        // Extension methods for retrieving the session state behavior stored on a route instance.
        public static SessionStateBehavior GetSessionStateBehavior(this IHttpRoute route)
        {
            return GetSessionStateBehavior(route.DataTokens);
        }

        public static SessionStateBehavior GetSessionStateBehavior(this RouteData routeData)
        {
            return GetSessionStateBehavior(routeData.DataTokens);
        }

        // Extension methods for retrieving the required UserSessionRole stored in route instance.
        public static UserSessionRole[] GetRequiredRoles(this IHttpRoute route)
        {
            return GetRequiredSessionRoles(route.DataTokens);
        }

        public static UserSessionRole[] GetRequiredRoles(this RouteData routeData)
        {
            return GetRequiredSessionRoles(routeData.DataTokens);
        }

        // Extension method overloads for MapHttpRoute under HttpRouteCollection instances.
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, SessionStateBehavior sessionBehavior)
        {
            return MapHttpRoute(routes, name, routeTemplate, defaults, null, sessionBehavior);
        }

        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, SessionStateBehavior sessionBehavior, UserSessionRole[] requiredRoles)
        {
            return MapHttpRoute(routes, name, routeTemplate, defaults, null, sessionBehavior, requiredRoles);
        }

        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, SessionStateBehavior sessionBehavior)
        {
            var route = routes.CreateRoute(routeTemplate, defaults, constraints);
            route.SetSessionStateBehavior(sessionBehavior);
            routes.Add(name, route);

            return route;
        }

        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate,
            object defaults, object constraints, SessionStateBehavior sessionBehavior, UserSessionRole[] requiredRoles)
        {
            var route = routes.CreateRoute(routeTemplate, defaults, constraints);
            route.SetSessionStateBehavior(sessionBehavior);
            route.SetRequiredSessionRoles(requiredRoles);
            routes.Add(name, route);

            return route;
        }

        // Retrieves the session state behavior from the dataTokens dictionary. Returns SessionStateBehavior.Default if no state can be found.
        private static SessionStateBehavior GetSessionStateBehavior(IDictionary<string, object> dataTokens)
        {
            return dataTokens.ContainsKey("SessionStateBehavior") ? (SessionStateBehavior)dataTokens["SessionStateBehavior"] : SessionStateBehavior.Default;
        }

        private static UserSessionRole[] GetRequiredSessionRoles(IDictionary<string, object> dataTokens)
        {
            return dataTokens.ContainsKey("RequiredRoles") ? (UserSessionRole[])dataTokens["RequiredRoles"] : new UserSessionRole[]{};
        }

        // Saves the specified session state behavior in the route's DataTokens dictionary.
        private static void SetSessionStateBehavior(this IHttpRoute route, SessionStateBehavior behavior)
        {
            route.DataTokens["SessionStateBehavior"] = behavior;
        }

        // Saves the specified UserSessionRoles in route's DataTokens dictionary.
        private static void SetRequiredSessionRoles(this IHttpRoute route, UserSessionRole[] requiredRoles)
        {
            route.DataTokens["RequiredRoles"] = requiredRoles;
        }
    }
}