namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CareersForms",
                c => new
                    {
                        PostFormId = c.Int(nullable: false, identity: true),
                        FormFName = c.String(),
                        FormLName = c.String(),
                        FormPhone = c.Int(nullable: false),
                        FormEmail = c.String(),
                        FormPostedDate = c.DateTime(nullable: false),
                        FormCoverLetter = c.String(),
                        FormResume = c.String(),
                        JobPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostFormId);
            
            CreateTable(
                "dbo.JobPostings",
                c => new
                    {
                        JobPostID = c.Int(nullable: false, identity: true),
                        JobPostTitle = c.String(),
                        JobPostDescription = c.String(),
                        JobPostDate = c.DateTime(nullable: false),
                        JobTypeID = c.Int(nullable: false),
                        JobDeptID = c.Int(nullable: false),
                        CareersForm_PostFormId = c.Int(),
                    })
                .PrimaryKey(t => t.JobPostID)
                .ForeignKey("dbo.JobDepartments", t => t.JobDeptID, cascadeDelete: true)
                .ForeignKey("dbo.JobTypes", t => t.JobTypeID, cascadeDelete: true)
                .ForeignKey("dbo.CareersForms", t => t.CareersForm_PostFormId)
                .Index(t => t.JobTypeID)
                .Index(t => t.JobDeptID)
                .Index(t => t.CareersForm_PostFormId);
            
            CreateTable(
                "dbo.JobDepartments",
                c => new
                    {
                        JobDeptID = c.Int(nullable: false, identity: true),
                        JobDeptName = c.String(),
                    })
                .PrimaryKey(t => t.JobDeptID);
            
            CreateTable(
                "dbo.JobTypes",
                c => new
                    {
                        JobTypeID = c.Int(nullable: false, identity: true),
                        JobTypeName = c.String(),
                    })
                .PrimaryKey(t => t.JobTypeID);
            
            CreateTable(
                "dbo.OnlineCheckIns",
                c => new
                    {
                        CheckInID = c.Int(nullable: false, identity: true),
                        PatientFName = c.String(),
                        PatientLName = c.String(),
                        Date = c.DateTime(nullable: false),
                        BookingID = c.Int(nullable: false),
                        PatientID = c.Int(nullable: false),
                        Patient_PatientID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CheckInID)
                .ForeignKey("dbo.Bookings", t => t.BookingID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientID)
                .Index(t => t.BookingID)
                .Index(t => t.Patient_PatientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnlineCheckIns", "Patient_PatientID", "dbo.Patients");
            DropForeignKey("dbo.OnlineCheckIns", "BookingID", "dbo.Bookings");
            DropForeignKey("dbo.JobPostings", "CareersForm_PostFormId", "dbo.CareersForms");
            DropForeignKey("dbo.JobPostings", "JobTypeID", "dbo.JobTypes");
            DropForeignKey("dbo.JobPostings", "JobDeptID", "dbo.JobDepartments");
            DropIndex("dbo.OnlineCheckIns", new[] { "Patient_PatientID" });
            DropIndex("dbo.OnlineCheckIns", new[] { "BookingID" });
            DropIndex("dbo.JobPostings", new[] { "CareersForm_PostFormId" });
            DropIndex("dbo.JobPostings", new[] { "JobDeptID" });
            DropIndex("dbo.JobPostings", new[] { "JobTypeID" });
            DropTable("dbo.OnlineCheckIns");
            DropTable("dbo.JobTypes");
            DropTable("dbo.JobDepartments");
            DropTable("dbo.JobPostings");
            DropTable("dbo.CareersForms");
        }
    }
}
