namespace Api.Models
{
    public class ProvinceModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
    }
}
