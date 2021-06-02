namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModify1 : DbMigration
    {
        public override void Up()
        {
            
            
            DropIndex("dbo.Photo", new[] { "Aritcle_Id1" });
           
           
        }
        
        public override void Down()
        {
            
            
            
            
            CreateIndex("dbo.Photo", "Aritcle_Id1");
           
        }
    }
}
