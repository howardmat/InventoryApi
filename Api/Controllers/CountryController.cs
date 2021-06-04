using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/country")]
    [ApiController]
    public class CountryController : InventoryControllerBase
    {
        private readonly CountryRequestService _countryRequestService;

        public CountryController(
            CountryRequestService countryRequestService)
        {
            _countryRequestService = countryRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> Get()
        {
            var serviceResponse = await _countryRequestService.ProcessListRequestAsync();
            return GetResultFromServiceResponse(serviceResponse);
        }
    }
}
