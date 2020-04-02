namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databasesetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        CurrentDate = c.DateTime(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Records", "BookingID", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Bookings", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.Records", new[] { "BookingID" });
            DropIndex("dbo.Bookings", new[] { "DoctorID" });
            DropIndex("dbo.Bookings", new[] { "PatientID" });
            DropTable("dbo.Records");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Bookings");
        }
    }
}
