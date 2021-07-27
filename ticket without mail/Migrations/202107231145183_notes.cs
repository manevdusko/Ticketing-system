namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.resolvedTickets", "note", c => c.String());
            AddColumn("dbo.Tickets", "note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "note");
            DropColumn("dbo.resolvedTickets", "note");
        }
    }
}
