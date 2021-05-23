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
    public class CountryController : ControllerBase
    {
        private readonly CountryService _countryService;

        public CountryController(
            CountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> Get()
        {
            // Get data from service
            var result = await _countryService.ListAsync();
            return this.GetResultFromServiceResponse(result);
        }
    }
}
