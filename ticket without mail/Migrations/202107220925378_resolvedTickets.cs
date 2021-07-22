namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resolvedTickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.resolvedTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false),
                        problemSubject = c.String(nullable: false),
                        problemBody = c.String(nullable: false),
                        ipv4 = c.String(),
                        submitTime = c.DateTime(nullable: false),
                        resolveTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.resolvedTickets");
        }
    }
}
