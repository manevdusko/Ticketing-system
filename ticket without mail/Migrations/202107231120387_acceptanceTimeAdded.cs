namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acceptanceTimeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.resolvedTickets", "acceptanceTime", c => c.String());
            AddColumn("dbo.Tickets", "acceptanceTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "acceptanceTime");
            DropColumn("dbo.resolvedTickets", "acceptanceTime");
        }
    }
}
