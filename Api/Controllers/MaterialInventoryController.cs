using Api.Models.Dto;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models.RequestModels;

namespace Api.Controllers
{
    [Authorize]
    [Route("/material/inventory")]
    [ApiController]
    public class MaterialInventoryController : InventoryControllerBase
    {
        private readonly MaterialInventoryRequestService _materialInventoryRequestService;

        public MaterialInventoryController(
            MaterialInventoryRequestService materialInventoryRequestService,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _materialInventoryRequestService = materialInventoryRequestService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialInventoryTransactionModel>> Get(int id)
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialInventoryRequestService.ProcessGetRequestAsync(id, tenantId);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("/material/{materialId}/inventory")]
        public async Task<ActionResult<IEnumerable<MaterialInventoryTransactionModel>>> GetByMaterialId(int materialId)
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialInventoryRequestService.ProcessListRequestAsync(materialId, tenantId);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<MaterialInventoryTransactionModel>> Post(MaterialInventoryTransactionRequest model)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialInventoryRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "MaterialInventory", new { id = result.Data?.Id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialInventoryRequestService.ProcessDeleteRequestAsync(id, userId, tenantId);
            return result.ToActionResult();
        }
    }
}
