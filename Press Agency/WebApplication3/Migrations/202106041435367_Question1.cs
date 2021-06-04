namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Question1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Qustion = c.String(),
                        Answer = c.String(),
                        Editor_Id = c.Int(nullable: false),
                        Answer_Id = c.Int(nullable: false),
                        IsAnswer = c.Boolean(nullable: false),
                        Editor_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.Editor_Id)
                .Index(t => t.Editor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Editor_Id", "dbo.Person");
            DropIndex("dbo.Questions", new[] { "Editor_Id" });
            DropTable("dbo.Questions");
        }
    }
}
