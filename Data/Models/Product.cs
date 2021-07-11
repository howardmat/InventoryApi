using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Product : TenantInventoryBaseModel
    {
        public int? UnitOfMeasurementId { get; set; }
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? FormulaId { get; set; }
        public virtual Formula Formula { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageFilename { get; set; }
    }
}
