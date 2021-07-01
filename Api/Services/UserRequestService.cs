using Api.Handlers;
using Api.Models.Dto;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserRequestService
    {
        private readonly UserEntityService _userEntityService;

        public UserRequestService(
            UserEntityService userEntityService)
        {
            _userEntityService = userEntityService;
        }

        public async Task<ResponseHandler<UserModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ResponseHandler<UserModel>();

            // Fetch object
            response.Data = await _userEntityService.GetModelOrDefaultAsync(id);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate User object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<UserModel>> ProcessCreateRequestAsync(UserModel model, int modifyingUserId)
        {
            var response = new ResponseHandler<UserModel>();

            response.Data = await _userEntityService.CreateAsync(model.LocalId, model.Email, model.FirstName, model.LastName, modifyingUserId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the User object");
            }

            return response;
        }
    }
}
