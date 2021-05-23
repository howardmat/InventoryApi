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
    public class ProvinceService
    {
        private readonly ILogger<ProvinceService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProvinceService(
            ILogger<ProvinceService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ProvinceModel>>> ListAsync(int countryId)
        {
            var response = new ServiceResponse<IEnumerable<ProvinceModel>>();

            try
            {
                // Fetch data
                var data = await _unitOfWork.ProvinceRepository.ListAsync(countryId);

                // Add to collection
                var list = new List<ProvinceModel>();
                foreach (var item in data)
                {
                    list.Add(_mapper.Map<ProvinceModel>(item));
                }

                // Set response
                response.Data = list;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProvinceService.ListAsync - exception:{@Exception}", ex);
            }

            return response;
        }
    }
}
