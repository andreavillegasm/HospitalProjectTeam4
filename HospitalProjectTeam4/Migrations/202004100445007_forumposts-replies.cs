namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forumpostsreplies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        PostingDate = c.DateTime(nullable: false),
                        PostingTitle = c.String(),
                        PostingContent = c.String(),
                        PostingState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.ForumReplies",
                c => new
                    {
                        ReplyID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        ReplyDate = c.DateTime(nullable: false),
                        ReplyContent = c.String(),
                    })
                .PrimaryKey(t => t.ReplyID)
                .ForeignKey("dbo.ForumPosts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumReplies", "PostID", "dbo.ForumPosts");
            DropForeignKey("dbo.ForumPosts", "PatientID", "dbo.Patients");
            DropIndex("dbo.ForumReplies", new[] { "PostID" });
            DropIndex("dbo.ForumPosts", new[] { "PatientID" });
            DropTable("dbo.ForumReplies");
            DropTable("dbo.ForumPosts");
        }
    }
}
