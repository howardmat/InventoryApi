using Api.Models.Dto;
using Api.Models.Results;
using AutoMapper;
using Data;
using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services;

public class UnitOfMeasurementEntityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UnitOfMeasurementEntityService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResult<IEnumerable<UnitOfMeasurementModel>>> ListAsync()
    {
        var response = new ServiceResult<IEnumerable<UnitOfMeasurementModel>>();

        // Fetch data
        var data = await _unitOfWork.UnitOfMeasurementRepository.ListAsync();

        // Add to collection
        var list = new List<UnitOfMeasurementModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<UnitOfMeasurementModel>(item));
        }

        response.Data = list;

        return response;
    }

    public async Task<ServiceResult<UnitOfMeasurementModel>> GetModelOrDefaultAsync(int id)
    {
        var response = new ServiceResult<UnitOfMeasurementModel>();

        // Fetch object
        var unitOfMeasurement = await GetEntityOrDefaultAsync(id);
        if (unitOfMeasurement == null)
        {
            response.SetNotFound($"Unable to locate UnitOfMeasurement object ({id})");
            return response;
        }

        response.Data = _mapper.Map<UnitOfMeasurementModel>(unitOfMeasurement);

        return response;
    }

    private async Task<UnitOfMeasurement> GetEntityOrDefaultAsync(int id)
    {
        // Fetch object
        return await _unitOfWork.UnitOfMeasurementRepository.GetAsync(id);
    }
}
