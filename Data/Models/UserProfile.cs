using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class UserProfile : InventoryBaseModel
    {
        [MaxLength(1024)]
        public string LocalId { get; set; }

        [MaxLength(512)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string FirstName { get; set; }

        [MaxLength(256)]
        public string LastName { get; set; }

        public int? TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
