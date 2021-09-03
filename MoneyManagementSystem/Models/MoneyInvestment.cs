using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagementSystem.Models
{
    public class MoneyInvestment
    {
        [Key]
        public int Id { get; set; }
        public float dollar { get; set; }
        public float euro { get; set; }
        public float gold { get; set; }
        public float turkish_lira { get; set; }

        public override string ToString()
        {
            return
                "{\"dollar\":" + dollar +
                "\"euro\":" + euro +
                "\"gold\":" + gold +
                "\"turklish_lira\":" + turkish_lira + "}";
        }
    }
}