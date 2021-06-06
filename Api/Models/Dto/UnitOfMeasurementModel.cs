using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
{
    public class UnitOfMeasurementModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Abbreviation { get; set; }

        [Required]
        public MeasurementSystem System { get; set; }

        [Required]
        public MeasurementType Type { get; set; }
    }
}
