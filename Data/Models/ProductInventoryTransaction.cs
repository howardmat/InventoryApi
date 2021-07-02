namespace Data.Models
{
    public class ProductInventoryTransaction : TenantInventoryBaseModel
    {
        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
    }
}
