using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementSystem.Models
{
    public class CompanyInvestment
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int Number { get; set; }
        
    }
    
}