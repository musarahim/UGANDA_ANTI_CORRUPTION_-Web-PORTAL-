using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    [Authorize(Users = "pr@igg.go.ug")]
    public class ReporterController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reporter
        public ActionResult Index()
        {
            var reporters = db.Reporters.ToList();
            return View(reporters);
        }
      
    }
}