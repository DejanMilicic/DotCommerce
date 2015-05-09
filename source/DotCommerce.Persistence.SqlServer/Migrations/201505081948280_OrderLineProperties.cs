namespace DotCommerce.Persistence.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderLineProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLine", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderLine", "Weight", c => c.Int(nullable: false));
            AddColumn("dbo.OrderLine", "ItemWeight", c => c.Int(nullable: false));
            AddColumn("dbo.OrderLine", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.OrderLine", "ImageUrl", c => c.String());
            AddColumn("dbo.OrderLine", "ItemUrl", c => c.String());
            AddColumn("dbo.OrderLine", "ItemPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLine", "ItemPrice");
            DropColumn("dbo.OrderLine", "ItemUrl");
            DropColumn("dbo.OrderLine", "ImageUrl");
            DropColumn("dbo.OrderLine", "Quantity");
            DropColumn("dbo.OrderLine", "ItemWeight");
            DropColumn("dbo.OrderLine", "Weight");
            DropColumn("dbo.OrderLine", "Discount");
        }
    }
}
