using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uganda_anti_corruption_portal.Models
{
    public class Contributor
    {
        public int ContributorID { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Display(Name ="Location/District")]
        public string Location { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ApplicationUser User { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }

    }
}