namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newspublish : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "NewsPublish", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "NewsPublish", c => c.Boolean(nullable: false));
        }
    }
}
