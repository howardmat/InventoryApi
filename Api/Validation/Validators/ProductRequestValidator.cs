using Api.Models.RequestModels;
using Data;
using Data.Enums;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class ProductRequestValidator : InventoryValidatorAsyncBase<ProductRequest>
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Product;

        private readonly IUnitOfWork _unitOfWork;

        public ProductRequestValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(ProductRequest model)
        {
            var isValid = true;

            var category = await _unitOfWork.CategoryRepository.GetAsync(model.CategoryId.Value);
            if (category == null || category.Type != CATEGORY_TYPE)
            {
                isValid = false;
                ServiceResponse.SetError("A valid Product Category could not be found with the CategoryId");
            }

            return isValid;
        }
    }
}
