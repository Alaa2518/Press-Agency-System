namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstCreataeDatabase1 : DbMigration
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
                        EditorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        AritclesID = c.Int(nullable: false),
                        Aritcle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.Aritcle_Id)
                .Index(t => t.Aritcle_Id);
            
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
                .ForeignKey("dbo.Person", t => t.Editor_Id)
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
            DropForeignKey("dbo.Questions", "Editor_Id", "dbo.Person");
            DropForeignKey("dbo.Photo", "Aritcle_Id", "dbo.Article");
            DropIndex("dbo.Questions", new[] { "Editor_Id" });
            DropIndex("dbo.Photo", new[] { "Aritcle_Id" });
            DropTable("dbo.UserRole");
            DropTable("dbo.Saving");
            DropTable("dbo.Questions");
            DropTable("dbo.Photo");
            DropTable("dbo.Person");
            DropTable("dbo.Article");
        }
    }
}
