using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class AddressModel
    {
        public int? Id { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(255)]
        public string City { get; set; }

        [Required]
        [MaxLength(15)]
        public string PostalCode { get; set; }

        [Required]
        public int? CountryId { get; set; }

        [Required]
        public int? ProvinceId { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual ProvinceModel Province { get; set; }
    }
}
