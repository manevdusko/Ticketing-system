namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acceptorInTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "acceptor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "acceptor");
        }
    }
}
