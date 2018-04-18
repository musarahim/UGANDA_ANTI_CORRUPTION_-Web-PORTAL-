using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class ReportCase
    {
        public int ReportCaseID { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0\d{9}|\+256\d{9})$",ErrorMessage ="Invalid Phone Number. Please input only Ugandan Number")]
        public string Phone { get; set; }
        [Display(Name ="Report About")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        public byte[] VideoData { get; set; }
        public string VideoType { get; set; }
        public byte[] AudioData { get; set; }
        public string AudioType { get; set; }
    }
}