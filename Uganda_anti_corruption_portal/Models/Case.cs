using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public enum CaseStatus { [Display(Name ="On Going")]OnGoing,Appealed,Concluded}
    public enum CaseCategory { Criminal,Civil}
    public class Case
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaseID { get; set; }
        [Display(Name = "Case Reference")]
        public string CaseRef { get; set; }
        [Display(Name ="Case Status")]
        public CaseStatus CaseStatus { get; set; }
        [Display(Name ="Case Category")]
        public CaseCategory CaseCategory { get; set; }
        [Display(Name ="Details"),DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name ="Date Updated:"),DataType(DataType.Date)]
        public DateTime UpdatedDate { get; set; }
    }
}