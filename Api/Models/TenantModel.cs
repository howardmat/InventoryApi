using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class TenantModel
    {
        public int? Id { get; set; }

        [MaxLength(255)]
        public string CompanyName { get; set; }

        public AddressModel PrimaryAddress { get; set; }
    }
}
