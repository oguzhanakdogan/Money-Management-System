using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace MoneyManagementSystem.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime Time { get; set; }
        public float bugdet { get; set; }
    }
}