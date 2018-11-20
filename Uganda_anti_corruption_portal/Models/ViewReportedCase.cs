using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class ViewReportedCase
    {
        public int ViewReportedCaseID { get; set; }
        public int ReportCaseID { get; set; }
        public virtual ReportCase ReportCase { get; set; }
        public int OfficeID { get; set; }
        public virtual Office Office { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }


    }
}