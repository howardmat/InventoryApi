namespace Data.Models
{
    public class FormulaIngredient : InventoryBaseModel
    {
        public int FormulaId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }

        public virtual Formula Formula { get; set; }
        public virtual Material Material { get; set; }
    }
}
