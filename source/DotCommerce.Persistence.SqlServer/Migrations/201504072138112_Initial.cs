namespace DotCommerce.Persistence.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(),
                        ProductName = c.String(),
                        Order_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLine", "Order_Id", "dbo.Order");
            DropIndex("dbo.OrderLine", new[] { "Order_Id" });
            DropTable("dbo.Order");
            DropTable("dbo.OrderLine");
        }
    }
}
