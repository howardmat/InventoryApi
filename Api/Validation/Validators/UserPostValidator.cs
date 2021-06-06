using Api.Models.Dto;
using Data;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class UserPostValidator : InventoryValidatorAsyncBase<UserModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserPostValidator(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<bool> IsValidAsync(UserModel item)
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
