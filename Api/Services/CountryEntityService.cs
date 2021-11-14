using Api.Models.Dto;
using Api.Models.Results;
using AutoMapper;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services;

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

    public async Task<ServiceResult<IEnumerable<CountryModel>>> ListAsync()
    {
        var result = new ServiceResult<IEnumerable<CountryModel>>();

        // Fetch data
        var data = await _unitOfWork.CountryRepository.ListAsync();

        // Add to collection
        var list = new List<CountryModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<CountryModel>(item));
        }

        result.Data = list;

        return result;
    }
}
