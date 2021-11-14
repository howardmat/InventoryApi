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
    [Route("/material")]
    [ApiController]
    public class MaterialController : InventoryControllerBase
    {
        private readonly MaterialEntityService _materialEntityService;
        private readonly MaterialRequestValidator _materialRequestValidator;

        public MaterialController(
            MaterialEntityService materialEntityService,
            MaterialRequestValidator materialRequestValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _materialEntityService = materialEntityService;
            _materialRequestValidator = materialRequestValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialModel>>> Get()
        {
            var user = await GetCurrentUserAsync(User);

            var result = await _materialEntityService.ListAsync(user.TenantId.Value);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialModel>> Get(int id)
        {
            var user = await GetCurrentUserAsync(User);

            var result = await _materialEntityService.GetModelOrDefaultAsync(id, user.TenantId.Value);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<MaterialModel>> Post(MaterialRequest model)
        {
            if (!await _materialRequestValidator.IsValidAsync(model))
                return _materialRequestValidator.ServiceResponse.ToActionResult();

            var user = await GetCurrentUserAsync(User);

            var result = await _materialEntityService.CreateAsync(model, user);
            return result.ToActionResult(
                Url.Action("Get", "Material", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MaterialRequest model)
        {
            if (!await _materialRequestValidator.IsValidAsync(model))
                return _materialRequestValidator.ServiceResponse.ToActionResult();

            var user = await GetCurrentUserAsync(User);

            var result = await _materialEntityService.UpdateAsync(id, model, user);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await GetCurrentUserAsync(User);

            var result = await _materialEntityService.DeleteAsync(id, user);
            return result.ToActionResult();
        }
    }
}
