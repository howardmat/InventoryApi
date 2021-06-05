using Api.Models;
using Data;
using Data.Enums;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class MaterialRequestValidator : InventoryValidatorAsyncBase<MaterialModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialRequestValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(MaterialModel model) 
        {
            return false;
        }

        public async Task<bool> IsValidAsync(MaterialModel model, CategoryType categoryType)
        {
            var isValid = true;

            var category = await _unitOfWork.CategoryRepository.GetAsync(model.CategoryId.Value);
            if (category == null || category.Type != categoryType)
            {
                isValid = false;
                ServiceResponse.SetError("A valid Material Category could not be found with the CategoryId");
            }

            return isValid;
        }
    }
}
