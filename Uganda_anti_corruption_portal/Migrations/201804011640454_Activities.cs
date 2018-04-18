namespace Uganda_anti_corruption_portal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Activities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityID = c.Int(nullable: false, identity: true),
                        ActivityCategoryID = c.Int(nullable: false),
                        ActivityNo = c.Int(nullable: false),
                        NameOfActivity = c.String(),
                        ImageData = c.Binary(),
                        ImageType = c.String(),
                        Description = c.String(),
                        ContributorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityID)
                .ForeignKey("dbo.ActivityCategories", t => t.ActivityCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Contributors", t => t.ContributorID, cascadeDelete: true)
                .Index(t => t.ActivityCategoryID)
                .Index(t => t.ContributorID);
            
            CreateTable(
                "dbo.ActivityCategories",
                c => new
                    {
                        ActivityCategoryID = c.Int(nullable: false, identity: true),
                        NameOfService = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.ActivityCategoryID);
            
            CreateTable(
                "dbo.Contributors",
                c => new
                    {
                        ContributorID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Location = c.String(),
                        Email = c.String(),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ContributorID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Location = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        CaseID = c.Int(nullable: false, identity: true),
                        CaseRef = c.String(),
                        CaseStatus = c.Int(nullable: false),
                        CaseCategory = c.Int(nullable: false),
                        Description = c.String(),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CaseID);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        OfficeID = c.Int(nullable: false, identity: true),
                        OfficeName = c.String(),
                        PhysicalLocation = c.String(),
                        BoxNumber = c.Int(nullable: false),
                        District = c.String(),
                        TelNo = c.String(),
                        Email = c.String(),
                        Fax = c.String(),
                    })
                .PrimaryKey(t => t.OfficeID);
            
            CreateTable(
                "dbo.ReportCases",
                c => new
                    {
                        ReportCaseID = c.Int(nullable: false, identity: true),
                        Phone = c.String(),
                        Contact = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        ImageData = c.Binary(),
                        ImageType = c.String(),
                        VideoData = c.Binary(),
                        VideoType = c.String(),
                        AudioData = c.Binary(),
                        AudioType = c.String(),
                    })
                .PrimaryKey(t => t.ReportCaseID);
            
            CreateTable(
                "dbo.Reports_Publication",
                c => new
                    {
                        Reports_PublicationID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        Details = c.String(),
                        FileName = c.String(),
                        FileContent = c.Binary(),
                    })
                .PrimaryKey(t => t.Reports_PublicationID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Activities", "ContributorID", "dbo.Contributors");
            DropForeignKey("dbo.Contributors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activities", "ActivityCategoryID", "dbo.ActivityCategories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Contributors", new[] { "ApplicationUserId" });
            DropIndex("dbo.Activities", new[] { "ContributorID" });
            DropIndex("dbo.Activities", new[] { "ActivityCategoryID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reports_Publication");
            DropTable("dbo.ReportCases");
            DropTable("dbo.Offices");
            DropTable("dbo.Cases");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Contributors");
            DropTable("dbo.ActivityCategories");
            DropTable("dbo.Activities");
        }
    }
}
