namespace DotCommerce.Persistence.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderLineProperties2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderLine", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderLine", "Weight", c => c.Int(nullable: false));
        }
    }
}
