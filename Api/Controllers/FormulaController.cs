using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[Route("/formula")]
[ApiController]
public class FormulaController : InventoryControllerBase
{
    private readonly FormulaEntityService _formulaEntityService;
    private readonly FormulaRequestValidator _formulaRequestValidator;

    public FormulaController(
        FormulaEntityService formulaEntityService,
        FormulaRequestValidator formulaRequestValidator,
        AuthenticationDetailService authDetailService) : base(authDetailService)
    {
        _formulaEntityService = formulaEntityService;
        _formulaRequestValidator = formulaRequestValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormulaModel>>> Get()
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _formulaEntityService.ListAsync(user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FormulaModel>> Get(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _formulaEntityService.GetModelOrDefaultAsync(id, user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<FormulaModel>> Post(FormulaRequest model)
    {
        if (!await _formulaRequestValidator.IsValidAsync(model))
            return _formulaRequestValidator.ServiceResponse.ToActionResult();

        var user = await GetCurrentUserAsync(User);

        var result = await _formulaEntityService.CreateAsync(
            model.Name,
            model.CategoryId.Value,
            model.Description,
            model.Ingredients,
            user);

        return result.ToActionResult(
            Url.Action("Get", "Formula", new { id = result.Data?.Id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FormulaRequest model)
    {
        if (!await _formulaRequestValidator.IsValidAsync(model))
            return _formulaRequestValidator.ServiceResponse.ToActionResult();

        var user = await GetCurrentUserAsync(User);

        var result = await _formulaEntityService.UpdateAsync(id, model.Name, model.CategoryId.Value, model.Description, user);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _formulaEntityService.DeleteAsync(id, user);
        return result.ToActionResult();
    }
}
