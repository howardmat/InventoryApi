namespace Api.Models.Dto
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryIsoCode { get; set; }
        public virtual CountryModel Country { get; set; }

        public string ProvinceIsoCode { get; set; }
        public virtual ProvinceModel Province { get; set; }
    }
}
