namespace ticket_without_mail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProblemType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProblemTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        problemName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProblemTypes");
        }
    }
}
