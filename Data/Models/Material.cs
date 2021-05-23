using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Material : InventoryBaseModel
    {
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        public int UnitOfMeasurementId { get; set; }
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string ImageFilename { get; set; }
    }
}
