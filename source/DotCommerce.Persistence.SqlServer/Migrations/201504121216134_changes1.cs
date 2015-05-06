namespace DotCommerce.Persistence.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLine", "ItemId", c => c.String());
            AddColumn("dbo.OrderLine", "ItemName", c => c.String());
            DropColumn("dbo.OrderLine", "ProductId");
            DropColumn("dbo.OrderLine", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderLine", "ProductName", c => c.String());
            AddColumn("dbo.OrderLine", "ProductId", c => c.String());
            DropColumn("dbo.OrderLine", "ItemName");
            DropColumn("dbo.OrderLine", "ItemId");
        }
    }
}
