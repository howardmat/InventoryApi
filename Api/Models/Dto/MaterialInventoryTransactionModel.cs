namespace Api.Models.Dto
{
    public class MaterialInventoryTransactionModel
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal AmountPaid { get; set; }
        public string Description { get; set; }

        public MaterialModel Material { get; set; }
    }
}
