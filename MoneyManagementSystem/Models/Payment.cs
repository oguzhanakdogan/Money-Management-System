using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementSystem.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public float electricity_bill { get; set; }
        public float water_bill { get; set; }
        public float gas_bill { get; set; }
        public float kitchen_charges { get; set; }
        
        
    }
}