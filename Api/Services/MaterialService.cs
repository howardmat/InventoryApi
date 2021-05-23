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
    public class MaterialService
    {
        private readonly ILogger<MaterialService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialService(
            ILogger<MaterialService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<MaterialModel>>> ListAsync()
        {
            var response = new ServiceResponse<IEnumerable<MaterialModel>>();

            try
            {
                // Fetch data
                var data = await _unitOfWork.MaterialRepository.ListAsync();

                // Add to collection
                var list = new List<MaterialModel>();
                foreach (var item in data)
                {
                    list.Add(_mapper.Map<MaterialModel>(item));
                }

                // Set response
                response.Data = list;
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialService.ListAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<MaterialModel>> GetAsync(int id)
        {
            var response = new ServiceResponse<MaterialModel>();

            try
            {
                // Fetch object
                var material = await _unitOfWork.MaterialRepository.GetAsync(id);

                // Set response
                if (material != null)
                {
                    response.Data = _mapper.Map<MaterialModel>(material);
                }
                else
                {
                    response.SetNotFound($"Unable to locate Material object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialService.ListAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<MaterialModel>> CreateAsync(MaterialModel model, int createdByUserId)
        {
            var response = new ServiceResponse<MaterialModel>();

            try
            {
                var now = DateTime.UtcNow;

                // Get tenant id from user
                var user = await _unitOfWork.UserRepository.FindByIdAsync(createdByUserId);
                var tenantId = user.TenantId;
                if (tenantId.HasValue)
                {
                    // Build and add the new object
                    var material = new Material
                    {
                        Name = model.Name,
                        CategoryId = model.CategoryId,
                        Description = model.Description,
                        UnitOfMeasurementId = model.UnitOfMeasurementId.Value,
                        TenantId = tenantId.Value,
                        CreatedUserId = createdByUserId,
                        LastModifiedUserId = createdByUserId,
                        CreatedUtc = now,
                        LastModifiedUtc = now
                    };

                    await _unitOfWork.MaterialRepository.AddAsync(material);

                    // Set response
                    if (await _unitOfWork.CompleteAsync() > 0)
                    {
                        response.Data = _mapper.Map<MaterialModel>(material);
                    }
                    else
                    {
                        response.SetError("An unexpected error occurred while saving the Material object");
                    }
                }
                else
                {
                    response.SetError("TenantId not set for User");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialService.CreateAsync - exception:{@Exception}", ex);

                response.SetException(ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(int id, MaterialModel model, int modifiedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var material = await _unitOfWork.MaterialRepository.GetAsync(id);
                if (material != null)
                {
                    // Update properties
                    material.Name = model.Name;
                    material.CategoryId = model.CategoryId;
                    material.Description = model.Description;
                    material.UnitOfMeasurementId = model.UnitOfMeasurementId.Value;
                    material.LastModifiedUserId = modifiedByUserId;
                    material.LastModifiedUtc = DateTime.UtcNow;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.SetError($"An unexpected error occurred while saving the Material object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Material object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialService.UpdateAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteAsync(int id, int deletedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var material = await _unitOfWork.MaterialRepository.GetAsync(id);
                if (material != null)
                {
                    material.DeletedUtc = DateTime.UtcNow;
                    material.DeletedUserId = deletedByUserId;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.SetError($"An unexpected error occurred while removing the Material object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Material object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialService.DeleteAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
