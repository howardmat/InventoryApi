using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("/product")]
    [ApiController]
    public class ProductController : InventoryControllerBase
    {
        //private readonly ProductRequestValidator _productRequestValidator;

        //public ProductController(
        //    ProductRequestValidator productRequestValidator,
        //    AuthenticationDetailService authDetailService) : base(authDetailService)
        //{
        //    _productRequestService = productRequestService;
        //    _productRequestValidator = productRequestValidator;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProductModel>>> Get()
        //{
        //    var result = await _productRequestService.ProcessListRequestAsync();
        //    return GetResultFromServiceResponse(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProductModel>> Get(int id)
        //{
        //    var result = await _productRequestService.ProcessGetRequestAsync(id);
        //    return GetResultFromServiceResponse(result);
        //}

        //[HttpPost]
        //public async Task<ActionResult<ProductModel>> Post(ProductModel model)
        //{
        //    if (!await _productRequestValidator.IsValidAsync(model))
        //        return GetResultFromServiceResponse(_productRequestValidator.ServiceResponse);

        //    var userId = await GetCurrentUserIdAsync(User);
        //    var tenantId = GetCurrentTenantId(User);

        //    var result = await _productRequestService.ProcessCreateRequestAsync(model, userId, tenantId);
        //    return GetResultFromServiceResponse(result,
        //        Url.Action("Get", "Material", new { id = result.Data?.Id }));
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, ProductModel model)
        //{
        //    if (!await _productRequestValidator.IsValidAsync(model))
        //        return GetResultFromServiceResponse(_productRequestValidator.ServiceResponse);

        //    var userId = await GetCurrentUserIdAsync(User);

        //    var result = await _productRequestService.ProcessUpdateRequestAsync(id, model, userId);
        //    return GetResultFromServiceResponse(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var userId = await GetCurrentUserIdAsync(User);

        //    var result = await _productRequestService.ProcessDeleteRequestAsync(id, userId);
        //    return GetResultFromServiceResponse(result);
        //}
    }
}
