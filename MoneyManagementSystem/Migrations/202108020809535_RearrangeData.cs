namespace MoneyManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RearrangeData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CompanyInvestment_Id", "dbo.CompanyInvestments");
            DropIndex("dbo.Customers", new[] { "CompanyInvestment_Id" });
            AddColumn("dbo.CompanyInvestments", "BrandName", c => c.String());
            AddColumn("dbo.CompanyInvestments", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyInvestments", "Customer_Id", c => c.Int());
            CreateIndex("dbo.CompanyInvestments", "Customer_Id");
            AddForeignKey("dbo.CompanyInvestments", "Customer_Id", "dbo.Customers", "Id");
            DropColumn("dbo.CompanyInvestments", "CocaCola");
            DropColumn("dbo.CompanyInvestments", "BMW");
            DropColumn("dbo.CompanyInvestments", "Mercedes");
            DropColumn("dbo.CompanyInvestments", "KocHolding");
            DropColumn("dbo.Customers", "CompanyInvestment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CompanyInvestment_Id", c => c.Int());
            AddColumn("dbo.CompanyInvestments", "KocHolding", c => c.Single(nullable: false));
            AddColumn("dbo.CompanyInvestments", "Mercedes", c => c.Single(nullable: false));
            AddColumn("dbo.CompanyInvestments", "BMW", c => c.Single(nullable: false));
            AddColumn("dbo.CompanyInvestments", "CocaCola", c => c.Single(nullable: false));
            DropForeignKey("dbo.CompanyInvestments", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.CompanyInvestments", new[] { "Customer_Id" });
            DropColumn("dbo.CompanyInvestments", "Customer_Id");
            DropColumn("dbo.CompanyInvestments", "Number");
            DropColumn("dbo.CompanyInvestments", "BrandName");
            CreateIndex("dbo.Customers", "CompanyInvestment_Id");
            AddForeignKey("dbo.Customers", "CompanyInvestment_Id", "dbo.CompanyInvestments", "Id");
        }
    }
}
