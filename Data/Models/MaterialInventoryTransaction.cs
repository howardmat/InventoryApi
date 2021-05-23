namespace Data.Models
{
    public class MaterialInventoryTransaction : InventoryBaseModel
    {
        public int MaterialId { get; set; }
        public int Quantity { get; set; }
        public decimal AmountPaid { get; set; }
        public string Description { get; set; }

        public virtual Material Material { get; set; }
    }
}
