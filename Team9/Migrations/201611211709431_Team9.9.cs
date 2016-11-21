namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team99 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "isDiscoutned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "isDiscoutned");
        }
    }
}
