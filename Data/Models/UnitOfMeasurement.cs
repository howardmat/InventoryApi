using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class UnitOfMeasurement : InventoryBaseModel
    {
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Abbreviation { get; set; }

        public MeasurementSystem System { get; set; }
        public MeasurementType Type { get; set; }
    }
}
