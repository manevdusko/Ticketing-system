namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resolver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.resolvedTickets", "resolver", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.resolvedTickets", "resolver");
        }
    }
}
