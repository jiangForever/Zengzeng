using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZengZeng
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                 name: "ShareImage",
                 url: "s/{unique}",
                 defaults: new { controller = "Card", action = "ShareImage", unique = UrlParameter.Optional }
             );

            routes.MapRoute(
               name: "CardDetail",
               url: "d/{unique}",
               defaults: new { controller = "Card", action = "Detail", unique = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}