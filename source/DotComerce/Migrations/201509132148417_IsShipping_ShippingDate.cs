namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsShipping_ShippingDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DotCommerce_Order", "IsShipping", c => c.Boolean(nullable: false));
            AddColumn("dbo.DotCommerce_Order", "ShippingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DotCommerce_Order", "ShippingDate");
            DropColumn("dbo.DotCommerce_Order", "IsShipping");
        }
    }
}
