using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
{
    public class TenantModel
    {
        public int? Id { get; set; }

        [MaxLength(255)]
        public string CompanyName { get; set; }

        [Required]
        public AddressModel PrimaryAddress { get; set; }
    }
}
