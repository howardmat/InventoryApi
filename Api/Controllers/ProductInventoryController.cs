using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("/product/inventory")]
    [ApiController]
    public class ProductInventoryController : InventoryControllerBase
    {
        private readonly ProductInventoryRequestService _productInventoryRequestService;

        public ProductInventoryController(
            ProductInventoryRequestService productInventoryRequestService,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _productInventoryRequestService = productInventoryRequestService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInventoryTransactionModel>> Get(int id)
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _productInventoryRequestService.ProcessGetRequestAsync(id, tenantId);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("/product/{productId}/inventory")]
        public async Task<ActionResult<IEnumerable<ProductInventoryTransactionModel>>> GetByProductId(int productId)
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _productInventoryRequestService.ProcessListRequestAsync(productId, tenantId);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<ProductInventoryTransactionModel>> Post(ProductInventoryTransactionRequest model)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _productInventoryRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "ProductInventory", new { id = result.Data?.Id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _productInventoryRequestService.ProcessDeleteRequestAsync(id, userId, tenantId);
            return result.ToActionResult();
        }
    }
}
