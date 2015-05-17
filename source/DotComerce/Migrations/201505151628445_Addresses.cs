namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addresses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
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
                        Email = c.String(),
                        Phone = c.String(),
                        SingleAddress = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Order", "BillingAddress_Id", c => c.Int());
            AddColumn("dbo.Order", "ShippingAddress_Id", c => c.Int());
            CreateIndex("dbo.Order", "BillingAddress_Id");
            CreateIndex("dbo.Order", "ShippingAddress_Id");
            AddForeignKey("dbo.Order", "BillingAddress_Id", "dbo.Address", "Id");
            AddForeignKey("dbo.Order", "ShippingAddress_Id", "dbo.Address", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ShippingAddress_Id", "dbo.Address");
            DropForeignKey("dbo.Order", "BillingAddress_Id", "dbo.Address");
            DropIndex("dbo.Order", new[] { "ShippingAddress_Id" });
            DropIndex("dbo.Order", new[] { "BillingAddress_Id" });
            DropColumn("dbo.Order", "ShippingAddress_Id");
            DropColumn("dbo.Order", "BillingAddress_Id");
            DropTable("dbo.Address");
        }
    }
}
