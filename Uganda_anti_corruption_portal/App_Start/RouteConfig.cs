using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Uganda_anti_corruption_portal
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
            routes.MapRoute(null,
                 "",
                 new
                {
                  controller = "Activities",
                  action = "Activity",
                  category = "Health",
                        page = 1 });
            routes.MapRoute(null,
            "Page{page}",
            new { controller = "Activities", action = "Activity", category = (string)null },
            new { page = @"\d+" }
                        );
            routes.MapRoute(null,
            "{category}",
            new { controller = "Activities", action = "Activity", page = 1 }
            );
            routes.MapRoute(null,
            "{category}/Page{page}",
            new { controller = "Activities", action = "Activity" },
            new { page = @"\d+" }
            );
            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}
