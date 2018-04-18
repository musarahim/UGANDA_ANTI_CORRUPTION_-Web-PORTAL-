using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class Enquiry
    {
        public int EnquiryID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Subject { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        [DataType(DataType.Date),Display(Name ="Enquiry Date")]
        public DateTime EnquiryDate { get; set; }
    }
}