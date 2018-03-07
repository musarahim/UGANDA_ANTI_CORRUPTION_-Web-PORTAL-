using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class ActivityCategory
    {
        public int ActivityCategoryID { get; set; }
        [Display(Name ="Public Service")]
        public string NameOfService { get; set; }
        public string Category { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}