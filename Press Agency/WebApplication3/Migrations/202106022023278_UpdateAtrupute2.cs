namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAtrupute2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photo", "Aritcle_Id1", "dbo.Article");
            DropIndex("dbo.Like", new[] { "person_Id" });
            DropIndex("dbo.Photo", new[] { "Aritcle_Id1" });
            AlterColumn("dbo.Like", "person_Id", c => c.Int());
            AlterColumn("dbo.Like", "person_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Like", "person_Id");
            DropTable("dbo.Photo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        Aritcle_Id = c.Int(nullable: false),
                        Aritcle_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("dbo.Like", new[] { "person_Id" });
            AlterColumn("dbo.Like", "person_ID", c => c.Int());
            AlterColumn("dbo.Like", "person_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Photo", "Aritcle_Id1");
            CreateIndex("dbo.Like", "person_Id");
            AddForeignKey("dbo.Photo", "Aritcle_Id1", "dbo.Article", "Id");
        }
    }
}
