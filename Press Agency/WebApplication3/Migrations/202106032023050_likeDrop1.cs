namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likeDrop1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        person_ID = c.Int(nullable: false),
                        ART_ID = c.Int(nullable: false),
                        article_Id = c.Int(),
                        per_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.ART_ID)
                .ForeignKey("dbo.Person", t => t.person_ID)
                .Index(t => t.ART_ID)
                .Index(t => t.person_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Like", "person_ID", "dbo.Person");
            DropForeignKey("dbo.Like", "ART_ID", "dbo.Article");
            DropIndex("dbo.Like", new[] { "person_ID" });
            DropIndex("dbo.Like", new[] { "ART_ID" });
            DropTable("dbo.Like");
        }
    }
}
