namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useridentitymodelscreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.String(nullable: false, maxLength: 128),
                        DoctorFName = c.String(),
                        DoctorMNmae = c.String(),
                        DoctorLNmae = c.String(),
                        DoctorBirthDate = c.DateTime(nullable: false),
                        DoctorEmail = c.String(),
                        DoctorPhone = c.String(),
                        DoctorAltPhone = c.String(),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.AspNetUsers", t => t.DoctorID)
                .Index(t => t.DoctorID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "DoctorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Patients", "PatientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.HospitalStaffs", "StaffID", "dbo.AspNetUsers");
            DropIndex("dbo.Patients", new[] { "PatientID" });
            DropIndex("dbo.HospitalStaffs", new[] { "StaffID" });
            DropIndex("dbo.Doctors", new[] { "DoctorID" });
            DropTable("dbo.Patients");
            DropTable("dbo.HospitalStaffs");
            DropTable("dbo.Doctors");
        }
    }
}
