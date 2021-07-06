namespace Api.Models.Dto
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public CountryModel Country { get; set; }
        public ProvinceModel Province { get; set; }
    }
}
