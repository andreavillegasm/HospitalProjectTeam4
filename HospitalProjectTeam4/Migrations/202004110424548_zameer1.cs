namespace HospitalProjectTeam4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zameer1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "CurrentDate", c => c.String());
            AlterColumn("dbo.Bookings", "BookingDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "BookingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "CurrentDate", c => c.DateTime(nullable: false));
        }
    }
}
