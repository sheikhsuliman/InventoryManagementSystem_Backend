namespace InventoryManagementSystem_Backend.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int QuantitySold { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
