namespace Api.Models.Dto
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }

        public CategoryModel Category { get; set; }
        public UnitOfMeasurementModel UnitOfMeasurement { get; set; }
        public FormulaModel Formula { get; set; }
    }
}