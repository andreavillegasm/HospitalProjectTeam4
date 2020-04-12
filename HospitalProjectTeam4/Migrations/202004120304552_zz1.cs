namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zz1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LostFounds", "LostorFound", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LostFounds", "LostorFound");
        }
    }
}
