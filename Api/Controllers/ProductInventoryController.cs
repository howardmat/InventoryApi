using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[Route("/product/inventory")]
[ApiController]
public class ProductInventoryController : InventoryControllerBase
{
    private readonly ProductInventoryTransactionService _productInventoryService;

    public ProductInventoryController(
        ProductInventoryTransactionService productInventoryService,
        AuthenticationDetailService authDetailService) : base(authDetailService)
    {
        _productInventoryService = productInventoryService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductInventoryTransactionModel>> Get(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _productInventoryService.GetModelOrDefaultAsync(id, user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpGet]
    [Route("/product/{productId}/inventory")]
    public async Task<ActionResult<IEnumerable<ProductInventoryTransactionModel>>> GetByProductId(int productId)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _productInventoryService.ListAsync(productId, user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<ProductInventoryTransactionModel>> Post(ProductInventoryTransactionRequest model)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _productInventoryService.CreateAsync(model, user);
        return result.ToActionResult(
            Url.Action("Get", "ProductInventory", new { id = result.Data?.Id }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _productInventoryService.DeleteAsync(id, user);
        return result.ToActionResult();
    }
}
