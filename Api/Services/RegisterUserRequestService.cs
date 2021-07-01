using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System.Threading.Tasks;

namespace Api.Services
{
    public class RegisterUserRequestService
    {
        private readonly UserEntityService _userEntityService;

        public RegisterUserRequestService(
            UserEntityService userEntityService)
        {
            _userEntityService = userEntityService;
        }

        public async Task<ResponseHandler<UserModel>> ProcessRegisterRequestAsync(RegisterUserRequest model)
        {
            var response = new ResponseHandler<UserModel>();

            response.Data = await _userEntityService.CreateAsync(model.LocalId, model.Email, model.FirstName, model.LastName);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the User object");
            }

            return response;
        }
    }
}
