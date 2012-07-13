using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Provider {

    public class RouteConfig {

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "User Identities",
                url: "user/{id}/{action}",
                defaults: new { controller = "User", action = "anon", id = UrlParameter.Optional, anon = false }
                );

            routes.MapRoute(
                name: "PPID Identifiers",
                url: "anon",
                defaults: new { controller = "User", action = "anon", id = UrlParameter.Optional, anon = true }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}