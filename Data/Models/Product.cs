namespace Data.Models
{
    public class Product : InventoryBaseModel
    {
        public int UnitOfMeasurementId { get; set; }
        public int? RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageFilename { get; set; }

        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
