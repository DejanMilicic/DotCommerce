namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressesCorrections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Country", c => c.String());
            AddColumn("dbo.Address", "State", c => c.String());
            AddColumn("dbo.Address", "Province", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "Province");
            DropColumn("dbo.Address", "State");
            DropColumn("dbo.Address", "Country");
        }
    }
}
