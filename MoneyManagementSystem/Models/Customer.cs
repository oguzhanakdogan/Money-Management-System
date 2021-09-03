using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public List<Payment> Payments { get; set; }
        public List<CompanyInvestment> CompanyInvestments { get; set; }
        public List<Budget> Budgets { get; set; }
        public MoneyInvestment MoneyInvestment { get; set; }
    }
}