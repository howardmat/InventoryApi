namespace Api.Models.Dto
{
    public class ProvinceModel
    {
        public string IsoCode { get; set; }
        public string Name { get; set; }

        public CountryModel Country { get; set; }
    }
}
