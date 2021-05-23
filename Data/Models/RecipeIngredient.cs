namespace Data.Models
{
    public class RecipeIngredient : InventoryBaseModel
    {
        public int RecipeId { get; set; }
        public int MaterialId { get; set; }
        public int Quantity { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Material Material { get; set; }
    }
}
