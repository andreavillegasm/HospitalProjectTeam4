namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        CurrentDate = c.String(),
                        BookingDate = c.String(),
                        PatientID = c.String(maxLength: 128),
                        DoctorID = c.String(maxLength: 128),
                        HospitalStaff_StaffID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.HospitalStaffs", t => t.HospitalStaff_StaffID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID)
                .Index(t => t.HospitalStaff_StaffID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.String(nullable: false, maxLength: 128),
                        DoctorFName = c.String(),
                        DoctorMName = c.String(),
                        DoctorLName = c.String(),
                        DoctorBirthDate = c.DateTime(nullable: false),
                        DoctorEmail = c.String(),
                        DoctorPhone = c.String(),
                        DoctorAltPhone = c.String(),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.AspNetUsers", t => t.DoctorID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsAdmin = c.Boolean(nullable: false),
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
                "dbo.HospitalStaffs",
                c => new
                    {
                        StaffID = c.String(nullable: false, maxLength: 128),
                        StaffFName = c.String(),
                        StaffMNmae = c.String(),
                        StaffNmae = c.String(),
                        StaffBirthDate = c.String(),
                        StaffEmail = c.String(),
                        StaffPhone = c.String(),
                    })
                .PrimaryKey(t => t.StaffID)
                .ForeignKey("dbo.AspNetUsers", t => t.StaffID)
                .Index(t => t.StaffID);
            
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
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.String(nullable: false, maxLength: 128),
                        PatientFName = c.String(),
                        PatientMNmae = c.String(),
                        PatientLNmae = c.String(),
                        PatientBirthDate = c.DateTime(nullable: false),
                        PatientEmail = c.String(),
                        PatientPhone = c.String(),
                        PatientAltPhone = c.String(),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PatientID = c.String(maxLength: 128),
                        PostingDate = c.DateTime(nullable: false),
                        PostingTitle = c.String(),
                        PostingContent = c.String(),
                        PostingState = c.Int(nullable: false),
                        PostingCategory = c.String(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.ForumReplies",
                c => new
                    {
                        ReplyID = c.Int(nullable: false, identity: true),
                        PatientID = c.String(maxLength: 128),
                        PostID = c.Int(nullable: false),
                        ReplyDate = c.DateTime(nullable: false),
                        ReplyContent = c.String(),
                    })
                .PrimaryKey(t => t.ReplyID)
                .ForeignKey("dbo.ForumPosts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.PatientID)
                .Index(t => t.PostID);
            
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
                "dbo.Records",
                c => new
                    {
                        RecordID = c.Int(nullable: false, identity: true),
                        RecordName = c.String(),
                        RecordType = c.String(),
                        RecordContent = c.String(),
                        HasFile = c.Int(nullable: false),
                        FileExtension = c.String(),
                        BookingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecordID)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)
                .Index(t => t.BookingID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsID = c.Int(nullable: false, identity: true),
                        NewsName = c.String(),
                        NewsDate = c.DateTime(nullable: false),
                        NewsPublish = c.String(),
                        NewsDescription = c.String(),
                        HasPic = c.Int(nullable: false),
                        PicExtension = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.LostFounds",
                c => new
                    {
                        LostFoundID = c.Int(nullable: false, identity: true),
                        LostorFound = c.String(),
                        LostFoundItem = c.String(),
                        LostFoundDate = c.String(),
                        LostFoundCategory = c.String(),
                        LostFoundColor = c.String(),
                        LostFoundPerson = c.String(),
                        LostFoundNote = c.String(),
                        PatientID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LostFoundID)
                .ForeignKey("dbo.Patients", t => t.PatientID)
                .Index(t => t.PatientID);
            
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
            DropForeignKey("dbo.LostFounds", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.News", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Records", "BookingID", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "DoctorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumPosts", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.ForumReplies", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.ForumReplies", "PostID", "dbo.ForumPosts");
            DropForeignKey("dbo.Bookings", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Patients", "PatientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "HospitalStaff_StaffID", "dbo.HospitalStaffs");
            DropForeignKey("dbo.HospitalStaffs", "StaffID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LostFounds", new[] { "PatientID" });
            DropIndex("dbo.News", new[] { "CategoryID" });
            DropIndex("dbo.Records", new[] { "BookingID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.ForumReplies", new[] { "PostID" });
            DropIndex("dbo.ForumReplies", new[] { "PatientID" });
            DropIndex("dbo.ForumPosts", new[] { "PatientID" });
            DropIndex("dbo.Patients", new[] { "PatientID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.HospitalStaffs", new[] { "StaffID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Doctors", new[] { "DoctorID" });
            DropIndex("dbo.Bookings", new[] { "HospitalStaff_StaffID" });
            DropIndex("dbo.Bookings", new[] { "DoctorID" });
            DropIndex("dbo.Bookings", new[] { "PatientID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LostFounds");
            DropTable("dbo.News");
            DropTable("dbo.Categories");
            DropTable("dbo.Records");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.ForumReplies");
            DropTable("dbo.ForumPosts");
            DropTable("dbo.Patients");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.HospitalStaffs");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Doctors");
            DropTable("dbo.Bookings");
        }
    }
}
