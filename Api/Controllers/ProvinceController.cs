using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<ProvinceModel>>> Get(string countryCode)
        {
            // Get data from service
            var result = await _provinceService.ProcessListRequestAsync(countryCode);
            return this.GetResultFromServiceResponse(result);
        }
    }
}
