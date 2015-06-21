namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Notes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DotCommerce_Order", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DotCommerce_Order", "Notes");
        }
    }
}
