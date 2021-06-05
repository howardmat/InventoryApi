using Api.Models.RequestModels;
using Data;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class TenantPostValidator : InventoryValidatorAsyncBase<TenantPost>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantPostValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(TenantPost item)
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
