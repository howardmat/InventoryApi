using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Tenant : InventoryBaseModel
    {
        [MaxLength(255)]
        public string CompanyName { get; set; }
        
        public int? OwnerUserId { get; set; }
        public virtual UserProfile OwnerUser { get; set; }

        public int? PrimaryAddressId { get; set; }
        public virtual Address PrimaryAddress { get; set; }

        public virtual ICollection<UserProfile> Users { get; set; } = new HashSet<UserProfile>();
    }
}
