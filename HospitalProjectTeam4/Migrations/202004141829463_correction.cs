namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "PatientMName", c => c.String());
            AddColumn("dbo.Patients", "PatientLName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "PatientLName");
            DropColumn("dbo.Patients", "PatientMName");
        }
    }
}
