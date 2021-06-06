using Api.Models;
using Api.Models.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UnitOfMeasurementRequestService
    {
        private readonly ILogger<UnitOfMeasurementRequestService> _logger;
        private readonly UnitOfMeasurementEntityService _unitOfMeasurementEntityService;

        public UnitOfMeasurementRequestService(
            ILogger<UnitOfMeasurementRequestService> logger,
            UnitOfMeasurementEntityService unitOfMeasurementEntityService)
        {
            _logger = logger;
            _unitOfMeasurementEntityService = unitOfMeasurementEntityService;
        }

        public async Task<ServiceResponse<IEnumerable<UnitOfMeasurementModel>>> ProcessListRequestAsync()
        {
            var response = new ServiceResponse<IEnumerable<UnitOfMeasurementModel>>();

            try
            {
                response.Data = await _unitOfMeasurementEntityService.ListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementRequestService.ProcessListRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<UnitOfMeasurementModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ServiceResponse<UnitOfMeasurementModel>();

            try
            {
                // Fetch object
                response.Data = await _unitOfMeasurementEntityService.GetModelOrDefaultAsync(id);
                if (response.Data == null)
                {
                    response.SetNotFound($"Unable to locate UnitOfMeasurement object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementRequestService.ProcessGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<UnitOfMeasurementModel>> ProcessCreateRequestAsync(UnitOfMeasurementModel model, int createdByUserId)
        {
            var response = new ServiceResponse<UnitOfMeasurementModel>();

            try
            {
                response.Data = await _unitOfMeasurementEntityService.CreateAsync(model.Name, model.Abbreviation, model.System, model.Type, createdByUserId);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while saving the UnitOfMeasurement object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementRequestService.ProcessCreateRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse> ProcessUpdateRequestAsync(int id, UnitOfMeasurementModel model, int modifiedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var unitOfMeasurement = await _unitOfMeasurementEntityService.GetEntityOrDefaultAsync(id);
                if (unitOfMeasurement != null)
                {
                    if (!await _unitOfMeasurementEntityService.UpdateAsync(unitOfMeasurement, model.Name, model.Abbreviation, modifiedByUserId))
                    {
                        response.SetError("An unexpected error occurred while saving the UnitOfMeasurement object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate UnitOfMeasurement object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementRequestService.ProcessUpdateRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse> ProcessDeleteRequestAsync(int id, int deletedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var unitOfMeasurement = await _unitOfMeasurementEntityService.GetEntityOrDefaultAsync(id);
                if (unitOfMeasurement != null)
                {
                    if (!await _unitOfMeasurementEntityService.DeleteAsync(unitOfMeasurement, deletedByUserId))
                    {
                        response.SetError("An unexpected error occurred while removing the UnitOfMeasurement object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate UnitOfMeasurement object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UnitOfMeasurementRequestService.ProcessDeleteRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
