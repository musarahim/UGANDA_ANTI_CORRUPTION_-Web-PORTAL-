using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public enum publicationType { publications,Reports,Legislation}
    public class Reports_Publication
    {
        public int Reports_PublicationID { get; set; }
        public string Title { get; set; }
        [Display(Name ="Type of publication")]
        public publicationType ReportsType { get; set; }
        [Display(Name = "Publication Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-mm-dd}",ApplyFormatInEditMode =true)]
        public DateTime PublicationDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }
        [Display(Name ="Upload File")]
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}