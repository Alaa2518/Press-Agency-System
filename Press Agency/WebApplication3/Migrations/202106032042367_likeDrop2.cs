namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likeDrop2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Like", "per_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Like", "per_Id", c => c.Int(nullable: false));
        }
    }
}
