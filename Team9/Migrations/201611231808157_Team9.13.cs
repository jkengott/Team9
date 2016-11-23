namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team913 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "isDiscounted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Albums", "DiscountAlbumPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "DiscountAlbumPrice");
            DropColumn("dbo.Albums", "isDiscounted");
        }
    }
}
