using Api.Models;
using Api.Services;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class UserPostValidator : InventoryValidatorAsyncBase<UserModel>
    {
        private readonly UserQueryService _userQueryService;

        public UserPostValidator(
            UserQueryService userQueryService)
        {
            _userQueryService = userQueryService;
        }

        public override async Task<bool> IsValidAsync(UserModel item)
        {
            var isValid = true;

            var userId = await _userQueryService.GetUserIdOrDefaultByAuthProviderIdAsync(item.LocalId);
            if (userId != null)
            {
                isValid = false;
                ServiceResponse.SetError("LocalId found for existing user");
            }

            return isValid;
        }
    }
}
