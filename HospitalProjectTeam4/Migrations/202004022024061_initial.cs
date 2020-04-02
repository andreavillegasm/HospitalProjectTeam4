namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Records");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        RecordID = c.Int(nullable: false, identity: true),
                        RecordName = c.String(),
                        RecordType = c.String(),
                        RecordContent = c.String(),
                        HasFile = c.Int(nullable: false),
                        FileExtension = c.String(),
                    })
                .PrimaryKey(t => t.RecordID);
            
        }
    }
}
