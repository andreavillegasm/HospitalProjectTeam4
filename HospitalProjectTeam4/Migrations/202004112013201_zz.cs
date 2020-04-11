namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zz : DbMigration
    {
        public override void Up()
        {
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
                "dbo.LostFounds",
                c => new
                    {
                        LostFoundID = c.Int(nullable: false, identity: true),
                        LostFoundItem = c.String(),
                        LostFoundDate = c.String(),
                        LostFoundCategory = c.String(),
                        LostFoundColor = c.String(),
                        LostFoundPerson = c.String(),
                        LostFoundNote = c.String(),
                        PatientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LostFoundID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            AddColumn("dbo.Bookings", "HospitalStaff_StaffID", c => c.Int());
            CreateIndex("dbo.Bookings", "HospitalStaff_StaffID");
            AddForeignKey("dbo.Bookings", "HospitalStaff_StaffID", "dbo.HospitalStaffs", "StaffID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LostFounds", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Bookings", "HospitalStaff_StaffID", "dbo.HospitalStaffs");
            DropIndex("dbo.LostFounds", new[] { "PatientID" });
            DropIndex("dbo.Bookings", new[] { "HospitalStaff_StaffID" });
            DropColumn("dbo.Bookings", "HospitalStaff_StaffID");
            DropTable("dbo.LostFounds");
            DropTable("dbo.HospitalStaffs");
        }
    }
}
