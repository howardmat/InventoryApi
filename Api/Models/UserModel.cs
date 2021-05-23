using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class UserModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        public int? TenantId { get; set; }
    }
}
