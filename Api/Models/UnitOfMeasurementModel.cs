using System.ComponentModel.DataAnnotations;

namespace Api.Models
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
    }
}
