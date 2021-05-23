using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Category : InventoryBaseModel
    {
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        public virtual CategoryType Type { get; set; }
    }
}
