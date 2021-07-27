namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class das : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.resolvedTickets", "days", c => c.Int(nullable: false));
            AddColumn("dbo.resolvedTickets", "hours", c => c.Int(nullable: false));
            AddColumn("dbo.resolvedTickets", "minutes", c => c.Int(nullable: false));
            AddColumn("dbo.resolvedTickets", "seconds", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.resolvedTickets", "seconds");
            DropColumn("dbo.resolvedTickets", "minutes");
            DropColumn("dbo.resolvedTickets", "hours");
            DropColumn("dbo.resolvedTickets", "days");
        }
    }
}
