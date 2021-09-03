using System.Web.UI.HtmlControls;

namespace MoneyManagementSystem.ViewModel
{
    public class Brands
    {
        public string Name { get; set; }
        public float PurchasePrice { get; set; }
        public float SalePrice { get; set; }
        public float Change { get; set; }
    }
}