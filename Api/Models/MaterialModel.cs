using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class MaterialModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required] 
        public int? CategoryId { get; set; }

        [Required] 
        public int? UnitOfMeasurementId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must have a positive value and cannot exceed {2}")]
        public double? Quantity { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }
    }
}
