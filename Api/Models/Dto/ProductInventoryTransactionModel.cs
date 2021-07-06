namespace Api.Models.Dto
{
    public class ProductInventoryTransactionModel
    {
        public int Id { get; set; }
        public int? OrderDetailId { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }

        public ProductModel Product { get; set; }
    }
}
