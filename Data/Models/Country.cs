using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Country : InventoryBaseModel
    {
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(2)]
        public string IsoCode { get; set; }
        public int DisplayOrder { get; set; }
    }
}
