using Api.Models;
using AutoMapper;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CountryService
    {
        private readonly ILogger<CountryService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(
            ILogger<CountryService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<CountryModel>>> ListAsync()
        {
            var response = new ServiceResponse<IEnumerable<CountryModel>>();

            try
            {
                // Fetch data
                var data = await _unitOfWork.CountryRepository.ListAsync();

                // Add to collection
                var list = new List<CountryModel>();
                foreach (var item in data)
                {
                    list.Add(_mapper.Map<CountryModel>(item));
                }

                // Set response
                response.Data = list;
            }
            catch (Exception ex)
            {
                _logger.LogError("CountryService.ListAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
