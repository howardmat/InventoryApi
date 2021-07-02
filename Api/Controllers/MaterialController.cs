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
        private readonly MaterialRequestService _materialRequestService;
        private readonly MaterialRequestValidator _materialRequestValidator;

        public MaterialController(
            MaterialRequestService materialRequestService,
            MaterialRequestValidator materialRequestValidator,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _materialRequestService = materialRequestService;
            _materialRequestValidator = materialRequestValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialModel>>> Get()
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialRequestService.ProcessListRequestAsync(tenantId);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialModel>> Get(int id)
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialRequestService.ProcessGetRequestAsync(id, tenantId);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<MaterialModel>> Post(MaterialRequest model)
        {
            if (!await _materialRequestValidator.IsValidAsync(model))
                return _materialRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "Material", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MaterialRequest model)
        {
            if (!await _materialRequestValidator.IsValidAsync(model))
                return _materialRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialRequestService.ProcessUpdateRequestAsync(id, model, userId, tenantId);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialRequestService.ProcessDeleteRequestAsync(id, userId, tenantId);
            return result.ToActionResult();
        }
    }
}
