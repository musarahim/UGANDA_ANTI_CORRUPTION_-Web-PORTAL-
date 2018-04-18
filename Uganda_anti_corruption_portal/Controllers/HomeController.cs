using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [RequireHttps]
        public ActionResult Index()
        {
          
            return View();
        }
        [Authorize(Users = "pr@igg.go.ug")]
        public ActionResult Administration()
        {

            return View();
        }

        public ActionResult About()
        {

            return View();
        }
    }
}