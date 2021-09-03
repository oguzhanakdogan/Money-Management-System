using System.Collections.Generic;
using MoneyManagementSystem.Models;
using Newtonsoft.Json.Linq;

namespace MoneyManagementSystem.ViewModel
{
    public class CustomerViewModel
    {
        public Customer Customer { get; set; }
        public List<Brands> Brands { get; set; }
        public Currencies Currencies { get; set; }
    }
}