namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisLike", "person_Id", c => c.Int(nullable: false));
            

        }

        public override void Down()
        {
            DropColumn("dbo.DisLike", "person_Id");
            

        }
    }
}
