using Api.Models.RequestModels;
using Data;
using Data.Enums;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class FormulaRequestValidator : InventoryValidatorAsyncBase<FormulaRequest>
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Formula;

        private readonly IUnitOfWork _unitOfWork;

        public FormulaRequestValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(FormulaRequest model)
        {
            var isValid = true;

            var category = await _unitOfWork.CategoryRepository.GetAsync(model.CategoryId.Value);
            if (category == null || category.Type != CATEGORY_TYPE)
            {
                isValid = false;
                ServiceResponse.SetError("A valid Formula Category could not be found with the CategoryId");
            }

            return isValid;
        }
    }
}
