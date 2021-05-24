namespace Data.Models
{
    public class Product : InventoryBaseModel
    {
        public int UnitOfMeasurementId { get; set; }
        public int CategoryId { get; set; }
        public int? FormulaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageFilename { get; set; }

        public virtual Category Category { get; set; }
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }
        public virtual Formula Formula { get; set; }
    }
}
