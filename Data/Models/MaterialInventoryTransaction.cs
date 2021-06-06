namespace Data.Models
{
    public class MaterialInventoryTransaction : InventoryBaseModel
    {
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AmountPaid { get; set; }
        public string Description { get; set; }

        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        public virtual Material Material { get; set; }
    }
}
