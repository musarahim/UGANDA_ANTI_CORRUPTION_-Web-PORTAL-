using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;

namespace Uganda_anti_corruption_portal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
           
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    [DbConfigurationType(typeof(MySqlEFConfiguration))]

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 
        static ApplicationDbContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }
        public ApplicationDbContext()
            : base("DefaultConnection",throwIfV1Schema:false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.Contributor> Contributors { get; set; }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.Activity> activities { get; set; }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.ActivityCategory> ActivityCategories { get; set; }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.ReportCase> ReportCases { get; set; }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.Case> Cases { get; set; }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.Office> Offices { get; set; }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.Reports_Publication> Reports_Publication { get; set; }

        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.Enquiry> Enquiries { get; set; }
        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.Reporter> Reporters { get; set; }
        public System.Data.Entity.DbSet<Uganda_anti_corruption_portal.Models.ViewReportedCase> ViewReportedCases { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Contributor>().
        //        HasOptional(c => c.Activities)
        //        .WithMany().
        //        WillCascadeOnDelete(true);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}