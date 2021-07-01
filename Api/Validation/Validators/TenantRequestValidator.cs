using Api.Models.RequestModels;
using Data;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class TenantRequestValidator : InventoryValidatorAsyncBase<TenantRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantRequestValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(TenantRequest item)
        {
            var isValid = true;

            var tenant = await _unitOfWork.TenantRepository.FindByOwnerIdAsync(item.OwnerUserId.Value);
            if (tenant != null)
            {
                isValid = false;
                ServiceResponse.SetError("OwnerUserId found for existing tenant");
            }

            return isValid;
        }
    }
}
