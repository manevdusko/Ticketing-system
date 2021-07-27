namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.resolvedTickets", "acceptanceTime", c => c.DateTime());
            AlterColumn("dbo.Tickets", "acceptanceTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "acceptanceTime", c => c.String());
            AlterColumn("dbo.resolvedTickets", "acceptanceTime", c => c.String());
        }
    }
}
