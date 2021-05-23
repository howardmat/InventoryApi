using System.Collections.Generic;

namespace Data.Models
{
    public class Formula : InventoryBaseModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<FormulaIngredient> Ingredients { get; set; } = new HashSet<FormulaIngredient>();
    }
}
