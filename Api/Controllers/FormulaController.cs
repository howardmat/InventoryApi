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
    [Route("/formula")]
    [ApiController]
    public class FormulaController : InventoryControllerBase
    {
        private readonly FormulaRequestService _formulaRequestService;
        private readonly FormulaRequestValidator _formulaRequestValidator;

        public FormulaController(
            FormulaRequestService formulaRequestService,
            FormulaRequestValidator formulaRequestValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _formulaRequestService = formulaRequestService;
            _formulaRequestValidator = formulaRequestValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormulaModel>>> Get()
        {
            var result = await _formulaRequestService.ProcessListRequestAsync();
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormulaModel>> Get(int id)
        {
            var result = await _formulaRequestService.ProcessGetRequestAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<FormulaModel>> Post(FormulaRequest model)
        {
            if (!await _formulaRequestValidator.IsValidAsync(model))
                return _formulaRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _formulaRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "Formula", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FormulaRequest model)
        {
            if (!await _formulaRequestValidator.IsValidAsync(model))
                return _formulaRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);

            var result = await _formulaRequestService.ProcessUpdateRequestAsync(id, model, userId);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);

            var result = await _formulaRequestService.ProcessDeleteRequestAsync(id, userId);
            return result.ToActionResult();
        }
    }
}
