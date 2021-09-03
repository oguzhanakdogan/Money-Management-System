namespace MoneyManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Budgets", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Budgets", "Time");
        }
    }
}
