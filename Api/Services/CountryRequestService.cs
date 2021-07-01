using Api.Handlers;
using Api.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CountryRequestService
    {
        private readonly CountryEntityService _countryEntityService;

        public CountryRequestService(
            CountryEntityService countryEntityService)
        {
            _countryEntityService = countryEntityService;
        }

        public async Task<ResponseHandler<IEnumerable<CountryModel>>> ProcessListRequestAsync()
        {
            var response = new ResponseHandler<IEnumerable<CountryModel>>();

            response.Data = await _countryEntityService.ListAsync();

            return response;
        }
    }
}
