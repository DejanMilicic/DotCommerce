namespace DotCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Action = c.String(),
                        Value = c.String(),
                        OldValue = c.String(),
                        EfOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.EfOrder_Id)
                .Index(t => t.EfOrder_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLog", "EfOrder_Id", "dbo.Order");
            DropIndex("dbo.OrderLog", new[] { "EfOrder_Id" });
            DropTable("dbo.OrderLog");
        }
    }
}
