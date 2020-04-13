namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeysadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "PatientID", c => c.String(maxLength: 128));
            AddColumn("dbo.Bookings", "DoctorID", c => c.String(maxLength: 128));
            AddColumn("dbo.Bookings", "HospitalStaff_StaffID", c => c.String(maxLength: 128));
            AddColumn("dbo.ForumPosts", "PatientID", c => c.String(maxLength: 128));
            AddColumn("dbo.ForumReplies", "PatientID", c => c.String(maxLength: 128));
            AddColumn("dbo.LostFounds", "PatientID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookings", "PatientID");
            CreateIndex("dbo.Bookings", "DoctorID");
            CreateIndex("dbo.Bookings", "HospitalStaff_StaffID");
            CreateIndex("dbo.ForumPosts", "PatientID");
            CreateIndex("dbo.ForumReplies", "PatientID");
            CreateIndex("dbo.LostFounds", "PatientID");
            AddForeignKey("dbo.Bookings", "HospitalStaff_StaffID", "dbo.HospitalStaffs", "StaffID");
            AddForeignKey("dbo.Bookings", "PatientID", "dbo.Patients", "PatientID");
            AddForeignKey("dbo.ForumReplies", "PatientID", "dbo.Patients", "PatientID");
            AddForeignKey("dbo.ForumPosts", "PatientID", "dbo.Patients", "PatientID");
            AddForeignKey("dbo.Bookings", "DoctorID", "dbo.Doctors", "DoctorID");
            AddForeignKey("dbo.LostFounds", "PatientID", "dbo.Patients", "PatientID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LostFounds", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Bookings", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.ForumPosts", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.ForumReplies", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Bookings", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Bookings", "HospitalStaff_StaffID", "dbo.HospitalStaffs");
            DropIndex("dbo.LostFounds", new[] { "PatientID" });
            DropIndex("dbo.ForumReplies", new[] { "PatientID" });
            DropIndex("dbo.ForumPosts", new[] { "PatientID" });
            DropIndex("dbo.Bookings", new[] { "HospitalStaff_StaffID" });
            DropIndex("dbo.Bookings", new[] { "DoctorID" });
            DropIndex("dbo.Bookings", new[] { "PatientID" });
            DropColumn("dbo.LostFounds", "PatientID");
            DropColumn("dbo.ForumReplies", "PatientID");
            DropColumn("dbo.ForumPosts", "PatientID");
            DropColumn("dbo.Bookings", "HospitalStaff_StaffID");
            DropColumn("dbo.Bookings", "DoctorID");
            DropColumn("dbo.Bookings", "PatientID");
        }
    }
}
