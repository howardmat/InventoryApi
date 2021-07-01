using Api.Models.RequestModels;
using Data;
using Data.Enums;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class MaterialRequestValidator : InventoryValidatorAsyncBase<MaterialRequest>
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Material;

        private readonly IUnitOfWork _unitOfWork;

        public MaterialRequestValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(MaterialRequest model)
        {
            var isValid = true;

            var category = await _unitOfWork.CategoryRepository.GetAsync(model.CategoryId.Value);
            if (category == null || category.Type != CATEGORY_TYPE)
            {
                isValid = false;
                ServiceResponse.SetError("A valid Material Category could not be found with the CategoryId");
            }

            return isValid;
        }
    }
}
