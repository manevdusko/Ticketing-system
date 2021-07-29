namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _290701 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.resolvedTickets", "problemType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.resolvedTickets", "problemType");
        }
    }
}
