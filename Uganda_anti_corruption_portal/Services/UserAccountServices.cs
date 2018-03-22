using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uganda_anti_corruption_portal.Models;

namespace Uganda_anti_corruption_portal.Services
{
    public class UserAccountServices
    {
        private ApplicationDbContext db;
        public UserAccountServices(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public void createUserAccount(string FirstName,string LastName,string Location,string Email,string userId)
        {
            var contributor = new Contributor { FirstName = FirstName, LastName =LastName, Location = Location, Email = Email, ApplicationUserId = userId };
            db.Contributors.Add(contributor);
            db.SaveChanges();
        }
    }
}