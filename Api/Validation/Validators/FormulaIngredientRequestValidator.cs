using Api.Models.RequestModels;

namespace Api.Validation.Validators
{
    public class FormulaIngredientRequestValidator : InventoryValidatorBase<FormulaIngredientRequest>
    {
        public override bool IsValid(FormulaIngredientRequest model)
        {
            var isValid = true;

            if (!model.FormulaId.HasValue)
            {
                isValid = false;
                ServiceResponse.SetError("FormulaId is a required field");
            }

            return isValid;
        }
    }
}
