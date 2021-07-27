namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usedTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "acceptanceTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.resolvedTickets", "totalTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.resolvedTickets", "totalTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Tickets", "acceptanceTime", c => c.DateTime());
        }
    }
}
