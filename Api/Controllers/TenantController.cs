using Api.Models;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : InventoryControllerBase
    {
        private readonly TenantRequestService _tenantRequestService;
        private readonly TenantPostValidator _tenantPostValidator;

        public TenantController(
            TenantRequestService tenantRequestService,
            TenantPostValidator tenantPostValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _tenantRequestService = tenantRequestService;
            _tenantPostValidator = tenantPostValidator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TenantModel>> Get(int id)
        {
            var result = await _tenantRequestService.ProcessGetRequestAsync(id);
            return GetResultFromServiceResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(TenantPost model)
        {
            if (!await _tenantPostValidator.IsValidAsync(model))
                return GetResultFromServiceResponse(_tenantPostValidator.ServiceResponse);

            var result = await _tenantRequestService.ProcessCreateRequestAsync(model, model.OwnerUserId.Value);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
