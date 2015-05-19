namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableRenaming : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Address", newName: "DotCommerceOrderAddress");
            RenameTable(name: "dbo.OrderLine", newName: "DotCommerceOrderLine");
            RenameTable(name: "dbo.Order", newName: "DotCommerceOrder");
            RenameTable(name: "dbo.OrderLog", newName: "DotCommerceOrderLog");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DotCommerceOrderLog", newName: "OrderLog");
            RenameTable(name: "dbo.DotCommerceOrder", newName: "Order");
            RenameTable(name: "dbo.DotCommerceOrderLine", newName: "OrderLine");
            RenameTable(name: "dbo.DotCommerceOrderAddress", newName: "Address");
        }
    }
}
