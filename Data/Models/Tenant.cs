using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Tenant : InventoryBaseModel
    {
        [MaxLength(255)]
        public string CompanyName { get; set; }
        
        public string OwnerUserId { get; set; }
        public virtual User OwnerUser { get; set; }

        public int PrimaryAddressId { get; set; }
        public virtual Address PrimaryAddress { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
