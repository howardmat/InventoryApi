using Api.Models.RequestModels;
using Data;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class UserRequestValidator : InventoryValidatorAsyncBase<UserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRequestValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(UserRequest item)
        {
            var isValid = true;

            var user = await _unitOfWork.UserRepository.FindByLocalIdAsync(item.LocalId);
            if (user != null)
            {
                isValid = false;
                ServiceResponse.SetError("LocalId found for existing user");
            }

            return isValid;
        }
    }
}
