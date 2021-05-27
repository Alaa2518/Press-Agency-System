namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstCreateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        RoleUser = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        photoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        Admin_Id = c.Int(),
                        Editor_Id = c.Int(),
                        Article_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admin", t => t.Admin_Id)
                .ForeignKey("dbo.Editor", t => t.Editor_Id)
                .ForeignKey("dbo.Article", t => t.Article_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Admin_Id)
                .Index(t => t.Editor_Id)
                .Index(t => t.Article_Id)
                .Index(t => t.User_Id);
            
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
                "dbo.Editor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QId = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        RoleUser = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        photoID = c.Int(nullable: false),
                        Article_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.Article_Id)
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
                        Editor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Editor", t => t.Editor_Id)
                .Index(t => t.Editor_Id);
            
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
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        RoleUser = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        photoID = c.Int(nullable: false),
                        Saving_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Saving", t => t.Saving_ID)
                .Index(t => t.Saving_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Saving_ID", "dbo.Saving");
            DropForeignKey("dbo.Photo", "User_Id", "dbo.User");
            DropForeignKey("dbo.Article", "Saving_ID", "dbo.Saving");
            DropForeignKey("dbo.Photo", "Article_Id", "dbo.Article");
            DropForeignKey("dbo.Editor", "Article_Id", "dbo.Article");
            DropForeignKey("dbo.Questions", "Editor_Id", "dbo.Editor");
            DropForeignKey("dbo.Photo", "Editor_Id", "dbo.Editor");
            DropForeignKey("dbo.Photo", "Admin_Id", "dbo.Admin");
            DropIndex("dbo.User", new[] { "Saving_ID" });
            DropIndex("dbo.Questions", new[] { "Editor_Id" });
            DropIndex("dbo.Editor", new[] { "Article_Id" });
            DropIndex("dbo.Article", new[] { "Saving_ID" });
            DropIndex("dbo.Photo", new[] { "User_Id" });
            DropIndex("dbo.Photo", new[] { "Article_Id" });
            DropIndex("dbo.Photo", new[] { "Editor_Id" });
            DropIndex("dbo.Photo", new[] { "Admin_Id" });
            DropTable("dbo.User");
            DropTable("dbo.Saving");
            DropTable("dbo.Questions");
            DropTable("dbo.Editor");
            DropTable("dbo.Article");
            DropTable("dbo.Photo");
            DropTable("dbo.Admin");
        }
    }
}
