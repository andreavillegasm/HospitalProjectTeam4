namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LostFounds", "picextension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LostFounds", "picextension");
        }
    }
}
