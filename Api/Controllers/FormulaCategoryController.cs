using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("/formula/category")]
    [ApiController]
    public class FormulaCategoryController : InventoryControllerBase
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Formula;

        private readonly CategoryRequestService _categoryRequestService;

        public FormulaCategoryController(
            CategoryRequestService categoryRequestService, 
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _categoryRequestService = categoryRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _categoryRequestService.ProcessListRequestAsync(CATEGORY_TYPE, tenantId);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            var tenantId = GetCurrentTenantId(User);

            var result = await _categoryRequestService.ProcessGetRequestAsync(CATEGORY_TYPE, id, tenantId);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> Post(CategoryRequest model)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _categoryRequestService.ProcessCreateRequestAsync(CATEGORY_TYPE, model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "FormulaCategory", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryRequest model)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _categoryRequestService.ProcessUpdateRequestAsync(CATEGORY_TYPE, id, model, userId, tenantId);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _categoryRequestService.ProcessDeleteRequestAsync(CATEGORY_TYPE, id, userId, tenantId);
            return result.ToActionResult();
        }
    }
}
