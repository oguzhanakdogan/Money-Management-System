namespace MoneyManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CompanyInvestments", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.MoneyInvestments", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.CompanyInvestments", new[] { "Customer_Id" });
            DropIndex("dbo.MoneyInvestments", new[] { "Customer_Id" });
            AddColumn("dbo.Customers", "CompanyInvestment_Id", c => c.Int());
            AddColumn("dbo.Customers", "MoneyInvestment_Id", c => c.Int());
            CreateIndex("dbo.Customers", "CompanyInvestment_Id");
            CreateIndex("dbo.Customers", "MoneyInvestment_Id");
            AddForeignKey("dbo.Customers", "CompanyInvestment_Id", "dbo.CompanyInvestments", "Id");
            AddForeignKey("dbo.Customers", "MoneyInvestment_Id", "dbo.MoneyInvestments", "Id");
            DropColumn("dbo.CompanyInvestments", "Customer_Id");
            DropColumn("dbo.MoneyInvestments", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MoneyInvestments", "Customer_Id", c => c.Int());
            AddColumn("dbo.CompanyInvestments", "Customer_Id", c => c.Int());
            DropForeignKey("dbo.Customers", "MoneyInvestment_Id", "dbo.MoneyInvestments");
            DropForeignKey("dbo.Customers", "CompanyInvestment_Id", "dbo.CompanyInvestments");
            DropIndex("dbo.Customers", new[] { "MoneyInvestment_Id" });
            DropIndex("dbo.Customers", new[] { "CompanyInvestment_Id" });
            DropColumn("dbo.Customers", "MoneyInvestment_Id");
            DropColumn("dbo.Customers", "CompanyInvestment_Id");
            CreateIndex("dbo.MoneyInvestments", "Customer_Id");
            CreateIndex("dbo.CompanyInvestments", "Customer_Id");
            AddForeignKey("dbo.MoneyInvestments", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.CompanyInvestments", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
