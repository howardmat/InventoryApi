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
            string countryIsoCode,
            string provinceIsoCode = null,
            int? modifyingUserId = null)
        {
            StreetAddress = streetAddress;
            City = city;
            PostalCode = postalCode;
            ProvinceIsoCode = provinceIsoCode;
            CountryIsoCode = countryIsoCode;

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

        [MaxLength(3)]
        public string ProvinceIsoCode { get; set; }
        public virtual Province Province { get; set; }

        [MaxLength(2)]
        public string CountryIsoCode { get; set; }
        public virtual Country Country { get; set; }

        public void Update(
            string street,
            string city,
            string postalCode,
            string countryIsoCode,
            string provinceIsoCode = null,
            int? modifyingUserId = null)
        {
            StreetAddress = street;
            City = city;
            PostalCode = postalCode;
            CountryIsoCode = countryIsoCode;
            ProvinceIsoCode = provinceIsoCode;

            LastModifiedUserId = modifyingUserId;
            LastModifiedUtc = DateTime.UtcNow;
        }
    }
}
