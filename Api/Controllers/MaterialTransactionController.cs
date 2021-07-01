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
    public class MaterialTransactionController : InventoryControllerBase
    {
        private readonly MaterialTransactionRequestService _materialTransactionRequestService;

        public MaterialTransactionController(
            MaterialTransactionRequestService materialTransactionRequestService,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _materialTransactionRequestService = materialTransactionRequestService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialInventoryTransactionModel>> Get(int id)
        {
            var result = await _materialTransactionRequestService.ProcessGetRequestAsync(id);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("/material/{materialId}/inventory")]
        public async Task<ActionResult<IEnumerable<MaterialInventoryTransactionModel>>> GetByMaterialId(int materialId)
        {
            var result = await _materialTransactionRequestService.ProcessListRequestAsync(materialId);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<MaterialInventoryTransactionModel>> Post(MaterialInventoryTransactionRequest model)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialTransactionRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "MaterialTransaction", new { id = result.Data?.Id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);

            var result = await _materialTransactionRequestService.ProcessDeleteRequestAsync(id, userId);
            return result.ToActionResult();
        }
    }
}
