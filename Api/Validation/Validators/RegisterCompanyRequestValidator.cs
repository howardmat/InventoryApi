using Api.Models;
using Data;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class RegisterCompanyRequestValidator : InventoryValidatorAsyncBase<int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCompanyRequestValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(int userId)
        {
            var isValid = true;

            var tenant = await _unitOfWork.TenantRepository.FindByOwnerIdAsync(userId);
            if (tenant != null)
            {
                isValid = false;
                ServiceResponse.SetError("OwnerUserId found for existing tenant");
            }

            return isValid;
        }
    }
}
