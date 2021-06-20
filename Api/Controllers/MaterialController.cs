using Api.Models.Dto;
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
            var result = await _materialRequestService.ProcessListRequestAsync();
            return GetResultFromServiceResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialModel>> Get(int id)
        {
            var result = await _materialRequestService.ProcessGetRequestAsync(id);
            return GetResultFromServiceResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<MaterialModel>> Post(MaterialModel model)
        {
            if (!await _materialRequestValidator.IsValidAsync(model))
                return GetResultFromServiceResponse(_materialRequestValidator.ServiceResponse);

            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _materialRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "Material", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MaterialModel model)
        {
            if (!await _materialRequestValidator.IsValidAsync(model))
                return GetResultFromServiceResponse(_materialRequestValidator.ServiceResponse);

            var userId = await GetCurrentUserIdAsync(User);

            var result = await _materialRequestService.ProcessUpdateRequestAsync(id, model, userId);
            return GetResultFromServiceResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);

            var result = await _materialRequestService.ProcessDeleteRequestAsync(id, userId);
            return GetResultFromServiceResponse(result);
        }
    }
}
