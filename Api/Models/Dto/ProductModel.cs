namespace Api.Models.Dto
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int UnitOfMeasurementId { get; set; }
        public int? FormulaId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }
    }
}