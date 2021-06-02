namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "Aritcle_Id1", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "Aritcle_Id1");
        }
    }
}
