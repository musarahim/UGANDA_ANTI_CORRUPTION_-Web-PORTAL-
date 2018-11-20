using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class Activity
    {
        //This class is used to store each step of a service 
        //services will be be stored step by step
        /// <summary>
        /// This table will be used by the IGG as steps that must be followed by the public to assess government services
        /// </summary>
        public int ActivityID { get; set; }
        [ForeignKey("ActivityCateory")]
        public int ActivityCategoryID { get; set; }
        public virtual ActivityCategory ActivityCateory { get; set; }
        [Display(Name="Step NO.")]
        public int ActivityNo { get; set; }
        [Display(Name ="Name of Activity")]
        public string NameOfActivity { get; set; }
        // will place the image here
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [ForeignKey("Office")]
        public int OfficeID { get; set; }
        public virtual Office Office { get; set; }

    }
}