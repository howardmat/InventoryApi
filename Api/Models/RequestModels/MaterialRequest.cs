using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class MaterialRequest
    {
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
