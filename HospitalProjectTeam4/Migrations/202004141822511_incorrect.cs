namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incorrect : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Patients", "PatientMNmae");
            DropColumn("dbo.Patients", "PatientLNmae");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "PatientLNmae", c => c.String());
            AddColumn("dbo.Patients", "PatientMNmae", c => c.String());
        }
    }
}
