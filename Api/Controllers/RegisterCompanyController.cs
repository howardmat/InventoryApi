using Api.Models.Dto;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/register/companyprofile")]
    [ApiController]
    public class RegisterCompanyController : InventoryControllerBase
    {
        private readonly RegisterCompanyRequestService _registerCompanyRequestService;
        private readonly RegisterCompanyPostValidator _registerCompanyPostValidator;

        public RegisterCompanyController(
            RegisterCompanyRequestService registerCompanyRequestService,
            RegisterCompanyPostValidator registerCompanyPostValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _registerCompanyRequestService = registerCompanyRequestService;
            _registerCompanyPostValidator = registerCompanyPostValidator;
        }

        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(TenantModel model)
        {
            var userId = await GetCurrentUserIdAsync(User);

            if (!await _registerCompanyPostValidator.IsValidAsync(userId))
                return GetResultFromServiceResponse(_registerCompanyPostValidator.ServiceResponse);

            var result = await _registerCompanyRequestService.ProcessRegisterRequestAsync(model, userId);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
