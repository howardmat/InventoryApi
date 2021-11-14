using Api.Models.Dto;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models.RequestModels;

namespace Api.Controllers;

[Authorize]
[Route("/material/inventory")]
[ApiController]
public class MaterialInventoryController : InventoryControllerBase
{
    private readonly MaterialInventoryTransactionService _materialInventoryService;

    public MaterialInventoryController(
        MaterialInventoryTransactionService materialInventoryService,
        AuthenticationDetailService authDetailService) : base(authDetailService)
    {
        _materialInventoryService = materialInventoryService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialInventoryTransactionModel>> Get(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _materialInventoryService.GetModelOrDefaultAsync(id, user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpGet]
    [Route("/material/{materialId}/inventory")]
    public async Task<ActionResult<IEnumerable<MaterialInventoryTransactionModel>>> GetByMaterialId(int materialId)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _materialInventoryService.ListAsync(materialId, user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<MaterialInventoryTransactionModel>> Post(MaterialInventoryTransactionRequest model)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _materialInventoryService.CreateAsync(model, user);
        return result.ToActionResult(
            Url.Action("Get", "MaterialInventory", new { id = result.Data?.Id }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _materialInventoryService.DeleteAsync(id, user);
        return result.ToActionResult();
    }
}
