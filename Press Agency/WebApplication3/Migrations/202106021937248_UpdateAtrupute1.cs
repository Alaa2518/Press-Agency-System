namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAtrupute1 : DbMigration
    {
        public override void Up()
        {   
            DropColumn("dbo.Photo", "AritclesID");
            DropColumn("dbo.Questions", "EditorAnswerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "EditorAnswerId", c => c.Int(nullable: false));
            AddColumn("dbo.Photo", "AritclesID", c => c.Int(nullable: false));
            
        }
    }
}
