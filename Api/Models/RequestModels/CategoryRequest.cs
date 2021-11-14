using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class CategoryRequest
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
