namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fgdjgoidjsiofdg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "acceptanceTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "acceptanceTime", c => c.DateTime(nullable: false));
        }
    }
}
