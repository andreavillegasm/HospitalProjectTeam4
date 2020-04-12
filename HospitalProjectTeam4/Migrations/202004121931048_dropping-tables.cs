namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droppingtables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Doctor_DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.AspNetUsers", "HospitalStaff_StaffID", "dbo.HospitalStaffs");
            DropForeignKey("dbo.AspNetUsers", "Patient_PatientID", "dbo.Patients");
            DropIndex("dbo.AspNetUsers", new[] { "Doctor_DoctorID" });
            DropIndex("dbo.AspNetUsers", new[] { "HospitalStaff_StaffID" });
            DropIndex("dbo.AspNetUsers", new[] { "Patient_PatientID" });
            DropColumn("dbo.AspNetUsers", "Doctor_DoctorID");
            DropColumn("dbo.AspNetUsers", "HospitalStaff_StaffID");
            DropColumn("dbo.AspNetUsers", "Patient_PatientID");
            DropTable("dbo.Doctors");
            DropTable("dbo.HospitalStaffs");
            DropTable("dbo.Patients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        PatientFName = c.String(),
                        PatientMNmae = c.String(),
                        PatientLNmae = c.String(),
                        PatientBirthDate = c.DateTime(nullable: false),
                        PatientEmail = c.String(),
                        PatientPhone = c.String(),
                        PatientAltPhone = c.String(),
                    })
                .PrimaryKey(t => t.PatientID);
            
            CreateTable(
                "dbo.HospitalStaffs",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        StaffFName = c.String(),
                        StaffMNmae = c.String(),
                        StaffNmae = c.String(),
                        StaffBirthDate = c.String(),
                        StaffEmail = c.String(),
                        StaffPhone = c.String(),
                    })
                .PrimaryKey(t => t.StaffID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        DoctorFName = c.String(),
                        DoctorMNmae = c.String(),
                        DoctorLNmae = c.String(),
                        DoctorBirthDate = c.DateTime(nullable: false),
                        DoctorEmail = c.String(),
                        DoctorPhone = c.String(),
                        DoctorAltPhone = c.String(),
                    })
                .PrimaryKey(t => t.DoctorID);
            
            AddColumn("dbo.AspNetUsers", "Patient_PatientID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "HospitalStaff_StaffID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Doctor_DoctorID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Patient_PatientID");
            CreateIndex("dbo.AspNetUsers", "HospitalStaff_StaffID");
            CreateIndex("dbo.AspNetUsers", "Doctor_DoctorID");
            AddForeignKey("dbo.AspNetUsers", "Patient_PatientID", "dbo.Patients", "PatientID");
            AddForeignKey("dbo.AspNetUsers", "HospitalStaff_StaffID", "dbo.HospitalStaffs", "StaffID");
            AddForeignKey("dbo.AspNetUsers", "Doctor_DoctorID", "dbo.Doctors", "DoctorID");
        }
    }
}
