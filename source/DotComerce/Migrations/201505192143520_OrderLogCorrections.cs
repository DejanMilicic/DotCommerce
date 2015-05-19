namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderLogCorrections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DotCommerceOrderLog", "EfOrder_Id", "dbo.DotCommerceOrder");
            DropIndex("dbo.DotCommerceOrderLog", new[] { "EfOrder_Id" });
            DropColumn("dbo.DotCommerceOrderLog", "OrderId");
            RenameColumn(table: "dbo.DotCommerceOrderLog", name: "EfOrder_Id", newName: "OrderId");
            AlterColumn("dbo.DotCommerceOrderLog", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.DotCommerceOrderLog", "OrderId");
            AddForeignKey("dbo.DotCommerceOrderLog", "OrderId", "dbo.DotCommerceOrder", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DotCommerceOrderLog", "OrderId", "dbo.DotCommerceOrder");
            DropIndex("dbo.DotCommerceOrderLog", new[] { "OrderId" });
            AlterColumn("dbo.DotCommerceOrderLog", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.DotCommerceOrderLog", name: "OrderId", newName: "EfOrder_Id");
            AddColumn("dbo.DotCommerceOrderLog", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.DotCommerceOrderLog", "EfOrder_Id");
            AddForeignKey("dbo.DotCommerceOrderLog", "EfOrder_Id", "dbo.DotCommerceOrder", "Id");
        }
    }
}
