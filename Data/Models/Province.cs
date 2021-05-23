using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Province : InventoryBaseModel
    {
        public int CountryId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(2)]
        public string IsoCode { get; set; }
        public int DisplayOrder { get; set; }

        public virtual Country Country { get; set; }
    }
}
