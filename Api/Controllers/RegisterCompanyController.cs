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
        private readonly TenantEntityService _tenantEntityService;
        private readonly RegisterCompanyRequestValidator _registerCompanyPostValidator;

        public RegisterCompanyController(
            TenantEntityService tenantEntityService,
            RegisterCompanyRequestValidator registerCompanyPostValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _tenantEntityService = tenantEntityService;
            _registerCompanyPostValidator = registerCompanyPostValidator;
        }

        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(RegisterCompanyRequest model)
        {
            var user = await GetCurrentUserAsync(User);

            if (!await _registerCompanyPostValidator.IsValidAsync(user.Id))
                return _registerCompanyPostValidator.ServiceResponse.ToActionResult();

            var result = await _tenantEntityService.RegisterNewAsync(model, user);
            return result.ToActionResult(
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
