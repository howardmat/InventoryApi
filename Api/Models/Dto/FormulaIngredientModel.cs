namespace Api.Models.Dto
{
    public class FormulaIngredientModel
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }

        public virtual MaterialModel Material { get; set; }
    }
}
