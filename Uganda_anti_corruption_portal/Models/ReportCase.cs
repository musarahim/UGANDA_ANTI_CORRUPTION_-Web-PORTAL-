using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class ReportCase
    {
        public int ReportCaseID { get; set; }
        public string Phone { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        public byte[] VideoData { get; set; }
        public string VideoType { get; set; }
        public byte[] AudioData { get; set; }
        public string AudioType { get; set; }
    }
}