using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class Office
    {
        public int OfficeID { get; set; }
        [Display(Name ="Office Name")]
        public string OfficeName { get; set; }
        [Display(Name ="Office Address")]
        public string PhysicalLocation { get; set; }
        [Display(Name ="P.O.BOX")]
        public int BoxNumber { get; set; }
        public string District { get; set; }
        [Display(Name ="Tel:")]
        public string TelNo { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Fax { get; set; }
    }
}