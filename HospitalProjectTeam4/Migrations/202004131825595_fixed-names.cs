namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixednames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "PatientMName", c => c.String());
            AddColumn("dbo.Patients", "PatientLNname", c => c.String());
            DropColumn("dbo.Patients", "PatientMNmae");
            DropColumn("dbo.Patients", "PatientLNmae");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "PatientLNmae", c => c.String());
            AddColumn("dbo.Patients", "PatientMNmae", c => c.String());
            DropColumn("dbo.Patients", "PatientLNname");
            DropColumn("dbo.Patients", "PatientMName");
        }
    }
}
