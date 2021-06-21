using Api.Models.Dto;
using Api.Services;
using Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("/material/category")]
    [ApiController]
    public class MaterialCategoryController : InventoryControllerBase
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Material;

        private readonly CategoryRequestService _categoryRequestService;

        public MaterialCategoryController(
            CategoryRequestService categoryRequestService,
            AuthenticationDetailService authDetailService) : base(authDetailService)
        {
            _categoryRequestService = categoryRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
        {
            var result = await _categoryRequestService.ProcessListRequestAsync(CATEGORY_TYPE);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            var result = await _categoryRequestService.ProcessGetRequestAsync(CATEGORY_TYPE, id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> Post(CategoryModel model)
        {
            var userId = await GetCurrentUserIdAsync(User);
            var tenantId = GetCurrentTenantId(User);

            var result = await _categoryRequestService.ProcessCreateRequestAsync(CATEGORY_TYPE, model, userId, tenantId);
            return result.ToActionResult(
                Url.Action("Get", "MaterialCategory", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryModel model)
        {
            var userId = await GetCurrentUserIdAsync(User);

            var result = await _categoryRequestService.ProcessUpdateRequestAsync(CATEGORY_TYPE, id, model, userId);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync(User);

            var result = await _categoryRequestService.ProcessDeleteRequestAsync(CATEGORY_TYPE, id, userId);
            return result.ToActionResult();
        }
    }
}
