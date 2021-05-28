namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstCreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleTitle = c.String(nullable: false),
                        ArticleBody = c.String(nullable: false),
                        CreationDate = c.String(),
                        ArticleType = c.String(nullable: false),
                        NumberOfViewers = c.Int(nullable: false),
                        NumberOfLikes = c.Int(nullable: false),
                        NumberOfDislikes = c.Int(nullable: false),
                        IfAproveed = c.Boolean(nullable: false),
                        ImageId = c.Int(nullable: false),
                        EditorId = c.Int(nullable: false),
                        Saving_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Saving", t => t.Saving_ID)
                .Index(t => t.Saving_ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        RoleUserID = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        photoID = c.Int(nullable: false),
                        Article_Id = c.Int(),
                        Saving_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.Article_Id)
                .ForeignKey("dbo.Saving", t => t.Saving_ID)
                .Index(t => t.Article_Id)
                .Index(t => t.Saving_ID);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        Person_Id = c.Int(),
                        Article_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.Person_Id)
                .ForeignKey("dbo.Article", t => t.Article_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ask = c.String(),
                        Answer = c.String(),
                        EditorAnswerId = c.Int(nullable: false),
                        IsAnswer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Saving",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "Saving_ID", "dbo.Saving");
            DropForeignKey("dbo.Article", "Saving_ID", "dbo.Saving");
            DropForeignKey("dbo.Photo", "Article_Id", "dbo.Article");
            DropForeignKey("dbo.Person", "Article_Id", "dbo.Article");
            DropForeignKey("dbo.Photo", "Person_Id", "dbo.Person");
            DropIndex("dbo.Photo", new[] { "Article_Id" });
            DropIndex("dbo.Photo", new[] { "Person_Id" });
            DropIndex("dbo.Person", new[] { "Saving_ID" });
            DropIndex("dbo.Person", new[] { "Article_Id" });
            DropIndex("dbo.Article", new[] { "Saving_ID" });
            DropTable("dbo.UserRole");
            DropTable("dbo.Saving");
            DropTable("dbo.Questions");
            DropTable("dbo.Photo");
            DropTable("dbo.Person");
            DropTable("dbo.Article");
        }
    }
}
