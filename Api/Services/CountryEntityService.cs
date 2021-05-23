using Api.Models;
using AutoMapper;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CountryEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryModel>> ListAsync()
        {
            // Fetch data
            var data = await _unitOfWork.CountryRepository.ListAsync();

            // Add to collection
            var list = new List<CountryModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<CountryModel>(item));
            }

            return list;
        }
    }
}
