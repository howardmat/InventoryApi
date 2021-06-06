using Api.Models;
using Api.Models.Dto;
using Api.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public class RegisterUserRequestService
    {
        private readonly ILogger<RegisterUserRequestService> _logger;
        private readonly UserEntityService _userEntityService;

        public RegisterUserRequestService(
            ILogger<RegisterUserRequestService> logger,
            UserEntityService userEntityService)
        {
            _logger = logger;
            _userEntityService = userEntityService;
        }

        public async Task<ServiceResponse<UserModel>> ProcessRegisterRequestAsync(RegisterUserPost model)
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
