using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class AddressRequest
    {
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
    }
}
