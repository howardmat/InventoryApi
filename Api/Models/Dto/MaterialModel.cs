using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
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

        public string Description { get; set; }
        public string ImageFilename { get; set; }
    }
}
