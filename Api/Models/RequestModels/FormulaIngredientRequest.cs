using System.ComponentModel.DataAnnotations;

namespace Api.Models.RequestModels
{
    public class FormulaIngredientRequest
    {
        public int? FormulaId { get; set; }

        [Required]
        public int? MaterialId { get; set; }

        [Required]
        public decimal? Quantity { get; set; }
    }
}
