using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class FormulaRequest
    {
        [Required]
        public int? CategoryId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }
        public string ImageFilename { get; set; }
    }
}
