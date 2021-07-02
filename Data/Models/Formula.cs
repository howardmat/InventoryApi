using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Formula : TenantInventoryBaseModel
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }

        public virtual ICollection<FormulaIngredient> Ingredients { get; set; } = new HashSet<FormulaIngredient>();
    }
}
