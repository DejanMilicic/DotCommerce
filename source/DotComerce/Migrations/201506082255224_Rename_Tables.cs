namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_Tables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DotCommerceOrderAddress", newName: "DotCommerce_OrderAddress");
            RenameTable(name: "dbo.DotCommerceOrderLine", newName: "DotCommerce_OrderLine");
            RenameTable(name: "dbo.DotCommerceOrder", newName: "DotCommerce_Order");
            RenameTable(name: "dbo.DotCommerceOrderLog", newName: "DotCommerce_OrderLog");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DotCommerce_OrderLog", newName: "DotCommerceOrderLog");
            RenameTable(name: "dbo.DotCommerce_Order", newName: "DotCommerceOrder");
            RenameTable(name: "dbo.DotCommerce_OrderLine", newName: "DotCommerceOrderLine");
            RenameTable(name: "dbo.DotCommerce_OrderAddress", newName: "DotCommerceOrderAddress");
        }
    }
}
