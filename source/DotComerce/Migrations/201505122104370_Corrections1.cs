namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Corrections1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OrderLinesPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Order", "Shipping", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Shipping");
            DropColumn("dbo.Order", "OrderLinesPrice");
        }
    }
}
