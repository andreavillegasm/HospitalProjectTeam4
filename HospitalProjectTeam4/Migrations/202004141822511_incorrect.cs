namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incorrect : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Patients", "PatientMName");
            DropColumn("dbo.Patients", "PatientLNname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "PatientLNname", c => c.String());
            AddColumn("dbo.Patients", "PatientMName", c => c.String());
        }
    }
}
