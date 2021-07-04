using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    [Route("/tenant")]
    [ApiController]
    public class TenantController : InventoryControllerBase
    {
        private readonly TenantRequestService _tenantRequestService;
        private readonly TenantRequestValidator _tenantRequestValidator;

        public TenantController(
            TenantRequestService tenantRequestService,
            TenantRequestValidator tenantRequestValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _tenantRequestService = tenantRequestService;
            _tenantRequestValidator = tenantRequestValidator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TenantModel>> Get(int id)
        {
            var result = await _tenantRequestService.ProcessGetRequestAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(TenantRequest model)
        {
            if (!await _tenantRequestValidator.IsValidAsync(model))
                return _tenantRequestValidator.ServiceResponse.ToActionResult();

            var result = await _tenantRequestService.ProcessCreateRequestAsync(model, model.OwnerUserId.Value);
            return result.ToActionResult(
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
