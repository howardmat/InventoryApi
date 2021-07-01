using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
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

        public async Task<ResponseHandler<TenantModel>> ProcessRegisterRequestAsync(RegisterCompanyRequest model, int createdByUserId)
        {
            var response = new ResponseHandler<TenantModel>();

            var assignCreatorAsOwner = true;

            var tenantRequest = new TenantRequest
            {
                CompanyName = model.CompanyName,
                PrimaryAddress = model.PrimaryAddress
            };

            response.Data = await _tenantEntityService.CreateAsync(tenantRequest, createdByUserId, assignCreatorAsOwner);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while registering the Company");
            }

            return response;
        }
    }
}
