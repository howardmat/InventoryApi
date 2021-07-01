using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class RegisterCompanyRequest
    {
        [MaxLength(255)]
        public string CompanyName { get; set; }

        [Required]
        public AddressRequest PrimaryAddress { get; set; }
    }
}
