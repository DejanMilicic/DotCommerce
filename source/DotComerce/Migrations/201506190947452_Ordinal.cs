namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ordinal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DotCommerce_Order", "Ordinal", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DotCommerce_Order", "Ordinal");
        }
    }
}
