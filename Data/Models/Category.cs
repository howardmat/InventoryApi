using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Category : TenantInventoryBaseModel
    {
        [MaxLength(256)]
        public string Name { get; set; }
        public virtual CategoryType Type { get; set; }
    }
}
