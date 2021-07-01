using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("/register/companyprofile")]
    [ApiController]
    public class RegisterCompanyController : InventoryControllerBase
    {
        private readonly RegisterCompanyRequestService _registerCompanyRequestService;
        private readonly RegisterCompanyRequestValidator _registerCompanyPostValidator;

        public RegisterCompanyController(
            RegisterCompanyRequestService registerCompanyRequestService,
            RegisterCompanyRequestValidator registerCompanyPostValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _registerCompanyRequestService = registerCompanyRequestService;
            _registerCompanyPostValidator = registerCompanyPostValidator;
        }

        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(RegisterCompanyRequest model)
        {
            var userId = await GetCurrentUserIdAsync(User);

            if (!await _registerCompanyPostValidator.IsValidAsync(userId))
                return _registerCompanyPostValidator.ServiceResponse.ToActionResult();

            var result = await _registerCompanyRequestService.ProcessRegisterRequestAsync(model, userId);
            return result.ToActionResult(
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
