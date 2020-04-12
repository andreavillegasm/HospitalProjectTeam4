namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class namechangeddoctors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DoctorMName", c => c.String());
            AddColumn("dbo.Doctors", "DoctorLName", c => c.String());
            DropColumn("dbo.Doctors", "DoctorMNmae");
            DropColumn("dbo.Doctors", "DoctorLNmae");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "DoctorLNmae", c => c.String());
            AddColumn("dbo.Doctors", "DoctorMNmae", c => c.String());
            DropColumn("dbo.Doctors", "DoctorLName");
            DropColumn("dbo.Doctors", "DoctorMName");
        }
    }
}
