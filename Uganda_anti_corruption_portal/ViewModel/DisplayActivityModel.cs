using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.ViewModel
{
    public class DisplayActivityModel
    {
        public IEnumerable<Contributor> Contributors { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public IEnumerable<ActivityCategory> ActivityCategories { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}