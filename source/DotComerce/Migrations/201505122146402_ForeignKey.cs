namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrderLine", name: "Order_Id", newName: "OrderId");
            RenameIndex(table: "dbo.OrderLine", name: "IX_Order_Id", newName: "IX_OrderId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OrderLine", name: "IX_OrderId", newName: "IX_Order_Id");
            RenameColumn(table: "dbo.OrderLine", name: "OrderId", newName: "Order_Id");
        }
    }
}
