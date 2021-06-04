using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class TenantPost
    {
        [MaxLength(255)]
        public string CompanyName { get; set; }

        [Required]
        public int? OwnerUserId { get; set; }

        [Required]
        public AddressModel PrimaryAddress { get; set; }
    }
}
