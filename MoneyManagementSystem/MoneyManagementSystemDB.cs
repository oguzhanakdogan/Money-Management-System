using System.Data.Entity;
using MoneyManagementSystem.Models;

namespace MoneyManagementSystem
{
    public class MoneyManagementSystemDB : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MoneyInvestment> MoneyInvestments { get; set; }
        public DbSet<CompanyInvestment> CompanyInvestments { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        
        
        public MoneyManagementSystemDB() : base("moneymanagement")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}