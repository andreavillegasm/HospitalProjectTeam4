namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsID = c.Int(nullable: false, identity: true),
                        NewsName = c.String(),
                        NewsDate = c.DateTime(nullable: false),
                        NewsPublish = c.Boolean(nullable: false),
                        NewsDescription = c.String(),
                        HasPic = c.Int(nullable: false),
                        PicExtension = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "CategoryID", "dbo.Categories");
            DropIndex("dbo.News", new[] { "CategoryID" });
            DropTable("dbo.News");
            DropTable("dbo.Categories");
        }
    }
}
