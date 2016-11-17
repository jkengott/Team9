namespace Team9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team98 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Purchases", "PurchaseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Purchases", "PurchaseDate", c => c.DateTime(nullable: false));
        }
    }
}
