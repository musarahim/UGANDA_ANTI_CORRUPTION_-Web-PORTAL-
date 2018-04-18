using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Controllers
{
    public class NavController : Controller
    {
        private ApplicationDbContext db =new ApplicationDbContext();
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = db.ActivityCategories.
                Select(x => x.Category).Distinct().OrderBy(x => x);
            return PartialView("FlexMenu", categories);
        }
    }
}