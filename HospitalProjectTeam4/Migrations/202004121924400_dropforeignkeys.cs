namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropforeignkeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Bookings", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.ForumPosts", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Bookings", "HospitalStaff_StaffID", "dbo.HospitalStaffs");
            DropForeignKey("dbo.LostFounds", "PatientID", "dbo.Patients");
            DropIndex("dbo.Bookings", new[] { "PatientID" });
            DropIndex("dbo.Bookings", new[] { "DoctorID" });
            DropIndex("dbo.Bookings", new[] { "HospitalStaff_StaffID" });
            DropIndex("dbo.ForumPosts", new[] { "PatientID" });
            DropIndex("dbo.LostFounds", new[] { "PatientID" });
            DropColumn("dbo.Bookings", "PatientID");
            DropColumn("dbo.Bookings", "DoctorID");
            DropColumn("dbo.Bookings", "HospitalStaff_StaffID");
            DropColumn("dbo.ForumPosts", "PatientID");
            DropColumn("dbo.LostFounds", "PatientID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LostFounds", "PatientID", c => c.Int(nullable: false));
            AddColumn("dbo.ForumPosts", "PatientID", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "HospitalStaff_StaffID", c => c.Int());
            AddColumn("dbo.Bookings", "DoctorID", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "PatientID", c => c.Int(nullable: false));
            CreateIndex("dbo.LostFounds", "PatientID");
            CreateIndex("dbo.ForumPosts", "PatientID");
            CreateIndex("dbo.Bookings", "HospitalStaff_StaffID");
            CreateIndex("dbo.Bookings", "DoctorID");
            CreateIndex("dbo.Bookings", "PatientID");
            AddForeignKey("dbo.LostFounds", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "HospitalStaff_StaffID", "dbo.HospitalStaffs", "StaffID");
            AddForeignKey("dbo.ForumPosts", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "PatientID", "dbo.Patients", "PatientID", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "DoctorID", "dbo.Doctors", "DoctorID", cascadeDelete: true);
        }
    }
}
