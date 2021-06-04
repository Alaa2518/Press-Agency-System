namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Question : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Editor_Id1", "dbo.Person");
            DropIndex("dbo.Questions", new[] { "Editor_Id1" });
            DropTable("dbo.Questions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ask = c.String(),
                        Answer = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                        Editor_Id = c.Int(nullable: false),
                        Editor_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Questions", "Editor_Id1");
            AddForeignKey("dbo.Questions", "Editor_Id1", "dbo.Person", "Id");
        }
    }
}
