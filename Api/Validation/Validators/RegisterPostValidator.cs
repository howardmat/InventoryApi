using Api.Models.RequestModels;
using Api.Services;
using System.Threading.Tasks;

namespace Api.Validation.Validators
{
    public class RegisterPostValidator : InventoryValidatorAsyncBase<RegisterPost>
    {
        private readonly UserQueryService _userQueryService;

        public RegisterPostValidator(
            UserQueryService userQueryService)
        {
            _userQueryService = userQueryService;
        }

        public override async Task<bool> IsValidAsync(RegisterPost item)
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
