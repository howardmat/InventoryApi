using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly ProvinceRequestService _provinceService;

        public ProvinceController(
            ProvinceRequestService provinceService)
        {
            _provinceService = provinceService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinceModel>>> Get(int countryId)
        {
            // Get data from service
            var result = await _provinceService.ListAsync(countryId);
            return this.GetResultFromServiceResponse(result);
        }
    }
}
