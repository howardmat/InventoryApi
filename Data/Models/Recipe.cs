using System.Collections.Generic;

namespace Data.Models
{
    public class Recipe : InventoryBaseModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<RecipeIngredient> Ingredients { get; set; } = new HashSet<RecipeIngredient>();
    }
}
