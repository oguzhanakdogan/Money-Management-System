namespace MoneyManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SystemSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        bugdet = c.Single(nullable: false),
                        Customer_Id = c.Int(),
                        
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.CompanyInvestments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CocaCola = c.Single(nullable: false),
                        BMW = c.Single(nullable: false),
                        Mercedes = c.Single(nullable: false),
                        KocHolding = c.Single(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoneyInvestments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dollar = c.Single(nullable: false),
                        euro = c.Single(nullable: false),
                        gold = c.Single(nullable: false),
                        turkish_lira = c.Single(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        electricity_bill = c.Single(nullable: false),
                        water_bill = c.Single(nullable: false),
                        gas_bill = c.Single(nullable: false),
                        kitchen_charges = c.Single(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.MoneyInvestments", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.CompanyInvestments", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Budgets", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Payments", new[] { "Customer_Id" });
            DropIndex("dbo.MoneyInvestments", new[] { "Customer_Id" });
            DropIndex("dbo.CompanyInvestments", new[] { "Customer_Id" });
            DropIndex("dbo.Budgets", new[] { "Customer_Id" });
            DropTable("dbo.Payments");
            DropTable("dbo.MoneyInvestments");
            DropTable("dbo.Customers");
            DropTable("dbo.CompanyInvestments");
            DropTable("dbo.Budgets");
        }
    }
}
