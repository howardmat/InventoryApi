using Api.Models;
using Api.Services;
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

        public TenantController(
            TenantRequestService tenantRequestService,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _tenantRequestService = tenantRequestService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TenantModel>> Get(int id)
        {
            // Get data from service
            var result = await _tenantRequestService.ProcessGetRequestAsync(id);
            return GetResultFromServiceResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(TenantModel model)
        {
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Create new record
            var result = await _tenantRequestService.ProcessCreateRequestAsync(model, userId);
            return this.GetResultFromServiceResponse(result,
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
