using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
{
    public class CategoryModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
