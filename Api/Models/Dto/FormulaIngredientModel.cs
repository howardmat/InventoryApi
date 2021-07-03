namespace Api.Models.Dto
{
    public class FormulaIngredientModel
    {
        public virtual MaterialModel Material { get; set; }

        public decimal Quantity { get; set; }
    }
}
