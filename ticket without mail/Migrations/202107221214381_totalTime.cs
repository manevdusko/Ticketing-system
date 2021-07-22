namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.resolvedTickets", "totalTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.resolvedTickets", "totalTime");
        }
    }
}
