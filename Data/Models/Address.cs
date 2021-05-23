using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Address : InventoryBaseModel
    {
        public string StreetAddress { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(15)]
        public string PostalCode { get; set; }

        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
