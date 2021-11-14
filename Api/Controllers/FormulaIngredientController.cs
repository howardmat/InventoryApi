using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[Route("/formula/ingredient")]
[ApiController]
public class FormulaIngredientController : InventoryControllerBase
{
    private readonly FormulaIngredientEntityService _formulaIngredientEntityService;
    private readonly FormulaIngredientRequestValidator _formulaIngredientRequestValidator;

    public FormulaIngredientController(
        FormulaIngredientEntityService formulaIngredientEntityService,
        FormulaIngredientRequestValidator formulaIngredientRequestValidator,
        AuthenticationDetailService authDetailService) : base(authDetailService)
    {
        _formulaIngredientEntityService = formulaIngredientEntityService;
        _formulaIngredientRequestValidator = formulaIngredientRequestValidator;
    }

    [HttpPost]
    public async Task<ActionResult<FormulaIngredientModel>> Post(FormulaIngredientRequest model)
    {
        if (!_formulaIngredientRequestValidator.IsValid(model))
            return _formulaIngredientRequestValidator.ServiceResponse.ToActionResult();

        var user = await GetCurrentUserAsync(User);

        var result = await _formulaIngredientEntityService.CreateAsync(model.FormulaId.Value, model.MaterialId.Value, model.Quantity.Value, user);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _formulaIngredientEntityService.DeleteAsync(id, user);
            return result.ToActionResult();
    }
}