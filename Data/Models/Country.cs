using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Country
    {
        [MaxLength(2)]
        [Key]
        public string IsoCode { get; set; }

        [MaxLength(1000)]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }
    }
}
