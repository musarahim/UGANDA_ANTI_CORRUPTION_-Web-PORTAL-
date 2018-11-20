using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class Reporter
    {
        public int ReporterID { get; set; }
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        public int ReportCaseID { get; set; }
        public virtual ReportCase ReportCase { get; set; }
    }
}