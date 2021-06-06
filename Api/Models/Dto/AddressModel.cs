using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
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
        public string CountryIsoCode { get; set; }

        [Required]
        public string ProvinceIsoCode { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual ProvinceModel Province { get; set; }
    }
}
