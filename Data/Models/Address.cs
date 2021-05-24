using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Address : InventoryBaseModel
    {
        public Address() { }

        public Address(
            string streetAddress,
            string city,
            string postalCode,
            int countryId,
            int? provinceId = null,
            int? modifyingUserId = null)
        {
            StreetAddress = streetAddress;
            City = city;
            PostalCode = postalCode;
            ProvinceId = provinceId;
            CountryId = countryId;

            var now = DateTime.Now;
            CreatedUserId = modifyingUserId;
            LastModifiedUserId = modifyingUserId;
            CreatedUtc = now;
            LastModifiedUtc = now;
        }

        public string StreetAddress { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(15)]
        public string PostalCode { get; set; }

        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public void Update(
            string street,
            string city,
            string postalCode,
            int countryId,
            int? provinceId = null,
            int? modifyingUserId = null)
        {
            StreetAddress = street;
            City = city;
            PostalCode = postalCode;
            CountryId = countryId;
            ProvinceId = provinceId;

            LastModifiedUserId = modifyingUserId;
            LastModifiedUtc = DateTime.UtcNow;
        }
    }
}
