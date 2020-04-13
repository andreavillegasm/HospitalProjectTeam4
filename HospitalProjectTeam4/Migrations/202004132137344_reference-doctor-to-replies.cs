namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class referencedoctortoreplies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ForumReplies", "PatientID", "dbo.Patients");
            DropIndex("dbo.ForumReplies", new[] { "PatientID" });
            AddColumn("dbo.ForumReplies", "DoctorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.ForumReplies", "DoctorID");
            AddForeignKey("dbo.ForumReplies", "DoctorID", "dbo.Doctors", "DoctorID");
            DropColumn("dbo.ForumReplies", "PatientID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ForumReplies", "PatientID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ForumReplies", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.ForumReplies", new[] { "DoctorID" });
            DropColumn("dbo.ForumReplies", "DoctorID");
            CreateIndex("dbo.ForumReplies", "PatientID");
            AddForeignKey("dbo.ForumReplies", "PatientID", "dbo.Patients", "PatientID");
        }
    }
}
