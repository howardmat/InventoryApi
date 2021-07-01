using Api.Handlers;
using Api.Models.Dto;
using System.Threading.Tasks;

namespace Api.Services
{
    public class RegisterCompanyRequestService
    {
        private readonly TenantEntityService _tenantEntityService;

        public RegisterCompanyRequestService(
            TenantEntityService tenantEntityService)
        {
            _tenantEntityService = tenantEntityService;
        }

        public async Task<ResponseHandler<TenantModel>> ProcessRegisterRequestAsync(TenantModel model, int createdByUserId)
        {
            var response = new ResponseHandler<TenantModel>();

            var assignCreatorAsOwner = true;

            response.Data = await _tenantEntityService.CreateAsync(model, createdByUserId, assignCreatorAsOwner);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while registering the Company");
            }

            return response;
        }
    }
}
