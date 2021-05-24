using Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserRequestService
    {
        private readonly ILogger<UserRequestService> _logger;
        private readonly UserEntityService _userEntityService;

        public UserRequestService(
            ILogger<UserRequestService> logger,
            UserEntityService userEntityService)
        {
            _logger = logger;
            _userEntityService = userEntityService;
        }

        public async Task<ServiceResponse<UserModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ServiceResponse<UserModel>();

            try
            {
                // Fetch object
                response.Data = await _userEntityService.GetModelOrDefaultAsync(id);
                if (response.Data == null)
                {
                    response.SetNotFound($"Unable to locate User object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UserRequestService.ProcessGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<UserModel>> ProcessCreateRequestAsync(UserModel model, int modifyingUserId)
        {
            var response = new ServiceResponse<UserModel>();

            try
            {
                response.Data = await _userEntityService.CreateAsync(model.Email, modifyingUserId);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while saving the User object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UserRequestService.ProcessCreateRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
