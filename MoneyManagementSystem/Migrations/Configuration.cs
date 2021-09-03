
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MoneyManagementSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MoneyManagementSystem.MoneyManagementSystemDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}