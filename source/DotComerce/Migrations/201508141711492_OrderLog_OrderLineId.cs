namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderLog_OrderLineId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DotCommerce_OrderLog", "OrderLineId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DotCommerce_OrderLog", "OrderLineId");
        }
    }
}
