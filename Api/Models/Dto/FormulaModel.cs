using System.Collections.Generic;

namespace Api.Models.Dto
{
    public class FormulaModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFilename { get; set; }

        public ICollection<FormulaIngredientModel> Ingredients { get; set; }
    }
}
