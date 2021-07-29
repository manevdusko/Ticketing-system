namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2907 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "note", c => c.String());
        }
    }
}
