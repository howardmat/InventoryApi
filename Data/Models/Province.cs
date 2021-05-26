using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Province
    {
        [MaxLength(3)]
        [Key]
        public string IsoCode { get; set; }

        [MaxLength(2)]
        public string CountryIsoCode { get; set; }

        [MaxLength(1000)]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Country Country { get; set; }
    }
}
