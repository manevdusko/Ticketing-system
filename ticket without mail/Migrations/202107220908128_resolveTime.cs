namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resolveTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "resolveTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "resolveTime");
        }
    }
}
