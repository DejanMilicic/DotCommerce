namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DotCommerce_OrderAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Company = c.String(),
                        Street = c.String(),
                        StreetNumber = c.String(),
                        City = c.String(),
                        Zip = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        Province = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        SingleAddress = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DotCommerce_OrderLine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.String(),
                        ItemName = c.String(),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemWeight = c.Int(nullable: false),
                        ItemUrl = c.String(),
                        ItemImageUrl = c.String(),
                        Weight = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DotCommerce_Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.DotCommerce_Order",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(),
                        Status = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        ItemsCount = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        OrderLinesPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Shipping = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillingAddress_Id = c.Int(),
                        ShippingAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DotCommerce_OrderAddress", t => t.BillingAddress_Id)
                .ForeignKey("dbo.DotCommerce_OrderAddress", t => t.ShippingAddress_Id)
                .Index(t => t.BillingAddress_Id)
                .Index(t => t.ShippingAddress_Id);
            
            CreateTable(
                "dbo.DotCommerce_OrderLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Action = c.String(),
                        Value = c.String(),
                        OldValue = c.String(),
                        OrderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DotCommerce_Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DotCommerce_Order", "ShippingAddress_Id", "dbo.DotCommerce_OrderAddress");
            DropForeignKey("dbo.DotCommerce_OrderLog", "OrderId", "dbo.DotCommerce_Order");
            DropForeignKey("dbo.DotCommerce_OrderLine", "OrderId", "dbo.DotCommerce_Order");
            DropForeignKey("dbo.DotCommerce_Order", "BillingAddress_Id", "dbo.DotCommerce_OrderAddress");
            DropIndex("dbo.DotCommerce_OrderLog", new[] { "OrderId" });
            DropIndex("dbo.DotCommerce_Order", new[] { "ShippingAddress_Id" });
            DropIndex("dbo.DotCommerce_Order", new[] { "BillingAddress_Id" });
            DropIndex("dbo.DotCommerce_OrderLine", new[] { "OrderId" });
            DropTable("dbo.DotCommerce_OrderLog");
            DropTable("dbo.DotCommerce_Order");
            DropTable("dbo.DotCommerce_OrderLine");
            DropTable("dbo.DotCommerce_OrderAddress");
        }
    }
}
