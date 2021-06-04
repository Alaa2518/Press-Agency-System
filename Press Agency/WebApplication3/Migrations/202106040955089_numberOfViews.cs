namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numberOfViews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NumberOfViews",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        article_Id = c.Int(nullable: false),
                        person_Id = c.Int(nullable: false),
                        article_Id1 = c.Int(),
                        person_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Article", t => t.article_Id)
                .ForeignKey("dbo.Person", t => t.person_Id)
                .Index(t => t.article_Id)
                .Index(t => t.person_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NumberOfViews", "person_Id", "dbo.Person");
            DropForeignKey("dbo.NumberOfViews", "article_Id", "dbo.Article");
            DropIndex("dbo.NumberOfViews", new[] { "person_Id" });
            DropIndex("dbo.NumberOfViews", new[] { "article_Id" });
            DropTable("dbo.NumberOfViews");
        }
    }
}
