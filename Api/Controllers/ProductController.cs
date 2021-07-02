using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("/product")]
    [ApiController]
    public class ProductController : InventoryControllerBase
    {
        private readonly ProductRequestService _productRequestService;
        private readonly ProductRequestValidator _productRequestValidator;

        public ProductController(
            ProductRequestService productRequestService,
            ProductRequestValidator productRequestValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _productRequestService = productRequestService;
            _productRequestValidator = productRequestValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _productRequestService.ProcessListRequestAsync(tenantId);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> Get(int id)
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _productRequestService.ProcessGetRequestAsync(id, tenantId);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Post(ProductRequest model)
        {
            if (!await _productRequestValidator.IsValidAsync(model))
                return _productRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _productRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "Product", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductRequest model)
        {
            if (!await _productRequestValidator.IsValidAsync(model))
                return _productRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _productRequestService.ProcessUpdateRequestAsync(id, model, userId, tenantId);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _productRequestService.ProcessDeleteRequestAsync(id, userId, tenantId);
            return result.ToActionResult();
        }
    }
}
