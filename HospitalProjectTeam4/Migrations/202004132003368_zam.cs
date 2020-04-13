namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zam : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "DoctorBirthDate", c => c.String());
            AlterColumn("dbo.Patients", "PatientBirthDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "PatientBirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Doctors", "DoctorBirthDate", c => c.DateTime(nullable: false));
        }
    }
}
