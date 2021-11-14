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
        private readonly ProductEntityService _productEntityService;
        private readonly ProductRequestValidator _productRequestValidator;

        public ProductController(
            ProductEntityService productEntityService,
            ProductRequestValidator productRequestValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _productEntityService = productEntityService;
            _productRequestValidator = productRequestValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
        {
            var user = await GetCurrentUserAsync(User);

            var result = await _productEntityService.ListAsync(user.TenantId.Value);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> Get(int id)
        {
            var user = await GetCurrentUserAsync(User);

            var result = await _productEntityService.GetModelOrDefaultAsync(id, user.TenantId.Value);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Post(ProductRequest model)
        {
            if (!await _productRequestValidator.IsValidAsync(model))
                return _productRequestValidator.ServiceResponse.ToActionResult();

            var user = await GetCurrentUserAsync(User);

            var result = await _productEntityService.CreateAsync(model, user);
            return result.ToActionResult(
                Url.Action("Get", "Product", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductRequest model)
        {
            if (!await _productRequestValidator.IsValidAsync(model))
                return _productRequestValidator.ServiceResponse.ToActionResult();

            var user = await GetCurrentUserAsync(User);

            var result = await _productEntityService.UpdateAsync(id, model, user);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await GetCurrentUserAsync(User);

            var result = await _productEntityService.DeleteAsync(id, user);
            return result.ToActionResult();
        }
    }
}
