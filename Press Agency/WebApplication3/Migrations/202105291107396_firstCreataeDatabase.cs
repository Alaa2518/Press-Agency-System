namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstCreataeDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photo", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.Person", "Article_Id", "dbo.Article");
            DropForeignKey("dbo.Photo", "Article_Id", "dbo.Article");
            DropForeignKey("dbo.Article", "Saving_ID", "dbo.Saving");
            DropForeignKey("dbo.Person", "Saving_ID", "dbo.Saving");
            DropIndex("dbo.Article", new[] { "Saving_ID" });
            DropIndex("dbo.Person", new[] { "Article_Id" });
            DropIndex("dbo.Person", new[] { "Saving_ID" });
            DropIndex("dbo.Photo", new[] { "Person_Id" });
            DropIndex("dbo.Photo", new[] { "Article_Id" });
            DropTable("dbo.Article");
            DropTable("dbo.Person");
            DropTable("dbo.Photo");
            DropTable("dbo.Questions");
            DropTable("dbo.Saving");
            DropTable("dbo.UserRole");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        Person_Id = c.Int(),
                        Article_Id = c.Int(),
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
                        photoID = c.Int(nullable: false),
                        Article_Id = c.Int(),
                        Saving_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Photo", "Article_Id");
            CreateIndex("dbo.Photo", "Person_Id");
            CreateIndex("dbo.Person", "Saving_ID");
            CreateIndex("dbo.Person", "Article_Id");
            CreateIndex("dbo.Article", "Saving_ID");
            AddForeignKey("dbo.Person", "Saving_ID", "dbo.Saving", "ID");
            AddForeignKey("dbo.Article", "Saving_ID", "dbo.Saving", "ID");
            AddForeignKey("dbo.Photo", "Article_Id", "dbo.Article", "Id");
            AddForeignKey("dbo.Person", "Article_Id", "dbo.Article", "Id");
            AddForeignKey("dbo.Photo", "Person_Id", "dbo.Person", "Id");
        }
    }
}
