namespace Api.Models.Dto
{
    public class TenantModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }
}
