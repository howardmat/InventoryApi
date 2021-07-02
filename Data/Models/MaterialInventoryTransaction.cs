namespace Data.Models
{
    public class MaterialInventoryTransaction : TenantInventoryBaseModel
    {
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        public decimal Quantity { get; set; }
        public decimal AmountPaid { get; set; }
        public string Description { get; set; }
    }
}
