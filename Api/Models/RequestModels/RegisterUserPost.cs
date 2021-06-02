using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class RegisterUserPost
    {
        [Required]
        [MaxLength(1024)]
        public string LocalId { get; set; }

        [Required]
        [MaxLength(512)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }
    }
}
