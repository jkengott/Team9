namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team94 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "DiscountPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "DiscountPrice");
        }
    }
}
