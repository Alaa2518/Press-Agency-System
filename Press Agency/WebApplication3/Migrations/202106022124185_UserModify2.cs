namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModify2 : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                        Aritcle_Id = c.Int(nullable: false),
                       
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.Aritcle_Id)
                .Index(t => t.Aritcle_Id);
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "Aritcle_Id", "dbo.Article");
            DropIndex("dbo.Photo", new[] { "Aritcle_Id" });
            
            DropTable("dbo.Photo");
           
        }
    }
}
