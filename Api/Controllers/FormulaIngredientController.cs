using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("/formula/ingredient")]
    [ApiController]
    public class FormulaIngredientController : InventoryControllerBase
    {
        private readonly FormulaIngredientRequestService _formulaIngredientRequestService;
        private readonly FormulaIngredientRequestValidator _formulaIngredientRequestValidator;

        public FormulaIngredientController(
            FormulaIngredientRequestService formulaIngredientRequestService,
            FormulaIngredientRequestValidator formulaIngredientRequestValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _formulaIngredientRequestService = formulaIngredientRequestService;
            _formulaIngredientRequestValidator = formulaIngredientRequestValidator;
        }

        [HttpPost]
        public async Task<ActionResult<FormulaIngredientModel>> Post(FormulaIngredientRequest model)
        {
            if (!_formulaIngredientRequestValidator.IsValid(model))
                return _formulaIngredientRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _formulaIngredientRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _formulaIngredientRequestService.ProcessDeleteRequestAsync(id, userId, tenantId);
            return result.ToActionResult();
        }
    }
}
