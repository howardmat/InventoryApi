using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class AddressModel
    {
        public int? Id { get; set; }
        public string StreetAddress { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(15)]
        public string PostalCode { get; set; }

        public virtual CountryModel Country { get; set; }
        public virtual ProvinceModel Province { get; set; }
    }
}
