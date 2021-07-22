namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "resolveTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "resolveTime", c => c.DateTime(nullable: false));
        }
    }
}
