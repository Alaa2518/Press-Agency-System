namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisLike", "article_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Like", "article_Id", c => c.Int(nullable: false));

        }

        public override void Down()
        {
            DropColumn("dbo.DisLike", "article_Id");
            DropColumn("dbo.Like", "article_Id");

        }
    }
}
