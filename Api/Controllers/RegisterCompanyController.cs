using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/register/companyprofile")]
    [ApiController]
    public class RegisterCompanyController : InventoryControllerBase
    {
        private readonly RegisterCompanyRequestService _registerCompanyRequestService;

        public RegisterCompanyController(
            RegisterCompanyRequestService registerCompanyRequestService,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _registerCompanyRequestService = registerCompanyRequestService;
        }

        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(TenantModel model)
        {
            var userId = await GetCurrentUserIdAsync(User);

            var result = await _registerCompanyRequestService.ProcessRegisterRequestAsync(model, userId);
            return this.GetResultFromServiceResponse(result,
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
