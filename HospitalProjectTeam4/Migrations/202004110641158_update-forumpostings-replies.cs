namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateforumpostingsreplies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForumPosts", "PostingCategory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ForumPosts", "PostingCategory");
        }
    }
}
