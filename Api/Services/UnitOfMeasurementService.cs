using Api.Models;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UnitOfMeasurementService
    {
        private readonly ILogger<UnitOfMeasurementService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitOfMeasurementService(
            ILogger<UnitOfMeasurementService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<UnitOfMeasurementModel>>> ListAsync()
        {
            var response = new ServiceResponse<IEnumerable<UnitOfMeasurementModel>>();

            try
            {
                // Fetch data
                var data = await _unitOfWork.UnitOfMeasurementRepository.ListAsync();

                // Add to collection
                var list = new List<UnitOfMeasurementModel>();
                foreach (var item in data)
                {
                    list.Add(_mapper.Map<UnitOfMeasurementModel>(item));
                }

                // Set response
                response.Data = list;
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementService.ListAsync - exception:{@Exception}", ex);
            }

            return response;
        }

        public async Task<ServiceResponse<UnitOfMeasurementModel>> GetAsync(int id)
        {
            var response = new ServiceResponse<UnitOfMeasurementModel>();

            try
            {
                // Fetch object
                var unitOfMeasurement = await _unitOfWork.UnitOfMeasurementRepository.GetAsync(id);

                // Set response
                if (unitOfMeasurement != null)
                {
                    response.Data = _mapper.Map<UnitOfMeasurementModel>(unitOfMeasurement);
                }
                else
                {
                    response.AddError($"Unable to locate UnitOfMeasurement object ({id})");
                    response.SetNotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementService.ListAsync - exception:{@Exception}", ex);
            }

            return response;
        }

        public async Task<ServiceResponse<UnitOfMeasurementModel>> CreateAsync(UnitOfMeasurementModel model, int createdByUserId)
        {
            var response = new ServiceResponse<UnitOfMeasurementModel>();

            try
            {
                // Build and add the new object
                var now = DateTime.UtcNow;
                var unitOfMeasurement = new UnitOfMeasurement
                {
                    Name = model.Name,
                    Abbreviation = model.Abbreviation,
                    CreatedUserId = createdByUserId,
                    CreatedUtc = now,
                    LastModifiedUserId = createdByUserId,
                    LastModifiedUtc = now
                };

                await _unitOfWork.UnitOfMeasurementRepository.AddAsync(unitOfMeasurement);

                // Set response
                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    response.Data = _mapper.Map<UnitOfMeasurementModel>(unitOfMeasurement);
                }
                else
                {
                    response.AddError($"An unexpected error occurred while saving the UnitOfMeasurement object");
                    response.SetError();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementService.CreateAsync - exception:{@Exception}", ex);
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(int id, UnitOfMeasurementModel model, int modifiedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var unitOfMeasurement = await _unitOfWork.UnitOfMeasurementRepository.GetAsync(id);
                if (unitOfMeasurement != null)
                {
                    // Update entity
                    unitOfMeasurement.Name = model.Name;
                    unitOfMeasurement.Abbreviation = model.Abbreviation;
                    unitOfMeasurement.LastModifiedUserId = modifiedByUserId;
                    unitOfMeasurement.LastModifiedUtc = DateTime.UtcNow;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.AddError($"An unexpected error occurred while saving the UnitOfMeasurement object");
                        response.SetError();
                    }
                }
                else
                {
                    response.SetNotFound();
                    response.AddError($"Unable to locate UnitOfMeasurement object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementService.UpdateAsync - exception:{@Exception}", ex);
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteAsync(int id, int deletedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var unitOfMeasurement = await _unitOfWork.UnitOfMeasurementRepository.GetAsync(id);
                if (unitOfMeasurement != null)
                {
                    unitOfMeasurement.DeletedUtc = DateTime.UtcNow;
                    unitOfMeasurement.DeletedUserId = deletedByUserId;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.AddError($"An unexpected error occurred while removing the UnitOfMeasurement object");
                        response.SetError();
                    }
                }
                else
                {
                    response.SetNotFound();
                    response.AddError($"Unable to locate UnitOfMeasurement object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementService.DeleteAsync - exception:{@Exception}", ex);
            }

            return response;
        }
    }
}
