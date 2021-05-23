namespace Data.Models
{
    public class ProductInventoryTransaction : InventoryBaseModel
    {
        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
