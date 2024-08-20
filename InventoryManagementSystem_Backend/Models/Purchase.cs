namespace InventoryManagementSystem_Backend.Models
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public int ProductID { get; set; }
        public int QuantityPurchased { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
