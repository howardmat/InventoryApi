using Api.Models;
using Api.Models.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CountryRequestService
    {
        private readonly ILogger<CountryRequestService> _logger;
        private readonly CountryEntityService _countryEntityService;

        public CountryRequestService(
            ILogger<CountryRequestService> logger,
            CountryEntityService countryEntityService)
        {
            _logger = logger;
            _countryEntityService = countryEntityService;
        }

        public async Task<ServiceResponse<IEnumerable<CountryModel>>> ProcessListRequestAsync()
        {
            var response = new ServiceResponse<IEnumerable<CountryModel>>();

            try
            {
                response.Data = await _countryEntityService.ListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("CountryRequestService.ProcessListRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
