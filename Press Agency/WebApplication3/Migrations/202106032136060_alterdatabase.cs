namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterdatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Like", "article_Id", "dbo.Article");
            DropForeignKey("dbo.Like", "person_Id", "dbo.Person");
            DropIndex("dbo.Like", new[] { "article_Id" });
            DropIndex("dbo.Like", new[] { "person_Id" });
            DropTable("dbo.Like");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        person_ID = c.Int(nullable: false),
                        ART_ID = c.Int(nullable: false),
                        article_Id = c.Int(),
                        person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Like", "person_Id");
            CreateIndex("dbo.Like", "article_Id");
            AddForeignKey("dbo.Like", "person_Id", "dbo.Person", "Id");
            AddForeignKey("dbo.Like", "article_Id", "dbo.Article", "Id");
        }
    }
}
