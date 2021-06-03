namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterdatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikesPost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pers_ID = c.Int(nullable: false),
                        ART_ID = c.Int(nullable: false),
                        article_Id = c.Int(),
                        person_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.ART_ID)
                .ForeignKey("dbo.Person", t => t.Pers_ID)
                .Index(t => t.ART_ID)
                .Index(t => t.Pers_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikesPost", "Pers_ID", "dbo.Person");
            DropForeignKey("dbo.LikesPost", "ART_ID", "dbo.Article");
            DropIndex("dbo.LikesPost", new[] { "Pers_ID" });
            DropIndex("dbo.LikesPost", new[] { "ART_ID" });
            DropTable("dbo.LikesPost");
        }
    }
}
