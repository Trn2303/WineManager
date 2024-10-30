using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WineManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "ProductIndex",
            url: "Product/{action}/{productType}/{vintage}/{region}",
            defaults: new { controller = "Product", action = "Index", productType = UrlParameter.Optional, vintage = UrlParameter.Optional, region = UrlParameter.Optional }
            );
        }
    }
}
