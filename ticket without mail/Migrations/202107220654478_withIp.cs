namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withIp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "ipv4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "ipv4");
        }
    }
}
