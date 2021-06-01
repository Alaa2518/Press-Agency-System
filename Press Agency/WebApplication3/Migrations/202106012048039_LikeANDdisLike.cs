namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeANDdisLike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DisLike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                       per_ID = c.Int(nullable: false),
                        art_ID = c.Int(nullable: false),
                        
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.art_ID)
                .ForeignKey("dbo.Person", t => t.per_ID)
                .Index(t => t.art_ID)
                .Index(t => t.per_ID);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        person_ID = c.Int(nullable: false),
                        ART_ID = c.Int(nullable: false),
                        
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.ART_ID)
                .ForeignKey("dbo.Person", t => t.person_ID)
                .Index(t => t.ART_ID)
                .Index(t => t.person_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Like", "person_ID", "dbo.Person");
            DropForeignKey("dbo.Like", "ART_ID", "dbo.Article");
            DropForeignKey("dbo.DisLike", "per_ID", "dbo.Person");
            DropForeignKey("dbo.DisLike", "art_ID", "dbo.Article");
            DropIndex("dbo.Like", new[] { "person_ID" });
            DropIndex("dbo.Like", new[] { "ART_ID" });
            DropIndex("dbo.DisLike", new[] { "per_ID" });
            DropIndex("dbo.DisLike", new[] { "art_ID" });
            DropTable("dbo.Like");
            DropTable("dbo.DisLike");
        }
    }
}
