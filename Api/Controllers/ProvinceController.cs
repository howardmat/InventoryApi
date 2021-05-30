using Api.Models;
using Api.Models.RequestModels;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : InventoryControllerBase
    {
        private readonly ProvinceRequestService _provinceService;

        public ProvinceController(
            ProvinceRequestService provinceService)
        {
            _provinceService = provinceService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceModel>>> Get([FromQuery]ProvinceGet model)
        {
            // Get data from service
            var result = await _provinceService.ProcessListRequestAsync(model.CountryIsoCode);
            return GetResultFromServiceResponse(result);
        }
    }
}
