namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cetvrta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Tickets", "problemSubject", c => c.String(nullable: false));
            AlterColumn("dbo.Tickets", "problemBody", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "problemBody", c => c.String());
            AlterColumn("dbo.Tickets", "problemSubject", c => c.String());
            AlterColumn("dbo.Tickets", "email", c => c.String());
        }
    }
}
