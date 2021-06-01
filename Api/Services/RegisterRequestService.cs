using Api.Models;
using Api.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public class RegisterRequestService
    {
        private readonly ILogger<RegisterRequestService> _logger;
        private readonly UserEntityService _userEntityService;

        public RegisterRequestService(
            ILogger<RegisterRequestService> logger,
            UserEntityService userEntityService)
        {
            _logger = logger;
            _userEntityService = userEntityService;
        }

        public async Task<ServiceResponse<UserModel>> ProcessRegisterRequestAsync(RegisterPost model)
        {
            var response = new ServiceResponse<UserModel>();

            try
            {
                response.Data = await _userEntityService.CreateAsync(model.LocalId, model.Email, model.FirstName, model.LastName);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while saving the User object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RegisterRequestService.ProcessRegisterRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
