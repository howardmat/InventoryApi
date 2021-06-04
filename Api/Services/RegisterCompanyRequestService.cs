using Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public class RegisterCompanyRequestService
    {
        private readonly ILogger<RegisterCompanyRequestService> _logger;
        private readonly TenantEntityService _tenantEntityService;

        public RegisterCompanyRequestService(
            ILogger<RegisterCompanyRequestService> logger,
            TenantEntityService tenantEntityService)
        {
            _logger = logger;
            _tenantEntityService = tenantEntityService;
        }

        public async Task<ServiceResponse<TenantModel>> ProcessRegisterRequestAsync(TenantModel model, int createdByUserId)
        {
            var response = new ServiceResponse<TenantModel>();

            try
            {
                var assignCreatorAsOwner = true;
                response.Data = await _tenantEntityService.CreateAsync(model, createdByUserId, assignCreatorAsOwner);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while registering the Company");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RegisterCompanyRequestService.ProcessRegisterRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
