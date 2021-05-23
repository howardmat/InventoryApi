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
    public class TenantService
    {
        private readonly ILogger<TenantService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TenantService(
            ILogger<TenantService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<TenantModel>>> ListAsync()
        {
            var response = new ServiceResponse<IEnumerable<TenantModel>>();

            try
            {
                // Fetch data
                var data = await _unitOfWork.TenantRepository.ListAsync();

                // Add to collection
                var list = new List<TenantModel>();
                foreach (var item in data)
                {
                    list.Add(_mapper.Map<TenantModel>(item));
                }

                // Set response
                response.Data = list;
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService.ListAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<TenantModel>> GetAsync(int id)
        {
            var response = new ServiceResponse<TenantModel>();

            try
            {
                // Fetch object
                var tenant = await _unitOfWork.TenantRepository.GetAsync(id);

                // Set response
                if (tenant != null)
                {
                    response.Data = _mapper.Map<TenantModel>(tenant);
                }
                else
                {
                    response.SetNotFound($"Unable to locate Tenant object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService.ListAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<TenantModel>> CreateAsync(TenantModel model, int createdByUserId)
        {
            var response = new ServiceResponse<TenantModel>();

            try
            {
                var now = DateTime.UtcNow;

                // Build and add the new object
                var tenant = new Tenant
                {
                    CompanyName = model.CompanyName,
                    OwnerUserId = createdByUserId,
                    PrimaryAddress = new Address
                    {
                        City = model.PrimaryAddress.City,
                        CountryId = model.PrimaryAddress.Country.Id,
                        PostalCode = model.PrimaryAddress.PostalCode,
                        ProvinceId = model.PrimaryAddress.Province.Id,
                        StreetAddress = model.PrimaryAddress.StreetAddress,
                        CreatedUserId = createdByUserId,
                        LastModifiedUserId = createdByUserId,
                        CreatedUtc = now,
                        LastModifiedUtc = now
                    },
                    CreatedUserId = createdByUserId,
                    LastModifiedUserId = createdByUserId,
                    CreatedUtc = now,
                    LastModifiedUtc = now
                };
                await _unitOfWork.TenantRepository.AddAsync(tenant);

                // Get the current user and set their tenant Id
                var user = await _unitOfWork.UserRepository.FindByIdAsync(createdByUserId);
                if (user != null)
                {
                    user.Tenant = tenant;

                    // Set response
                    if (await _unitOfWork.CompleteAsync() > 0)
                    {
                        tenant = await _unitOfWork.TenantRepository.GetAsync(tenant.Id);

                        response.Data = _mapper.Map<TenantModel>(tenant);
                    }
                    else
                    {
                        response.SetError($"An unexpected error occurred while saving the Tenant object");
                    }
                }
                else
                {
                    response.SetError($"Unable to locate the User. Failed to create the Tenant object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService.CreateAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(int id, TenantModel model, int modifiedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var tenant = await _unitOfWork.TenantRepository.GetAsync(id);
                if (tenant != null)
                {
                    // Update properties
                    tenant.CompanyName = model.CompanyName;
                    tenant.LastModifiedUserId = modifiedByUserId;
                    tenant.LastModifiedUtc = DateTime.UtcNow;

                    tenant.PrimaryAddress.City = model.PrimaryAddress.City;
                    tenant.PrimaryAddress.CountryId = model.PrimaryAddress.Country.Id;
                    tenant.PrimaryAddress.PostalCode = model.PrimaryAddress.PostalCode;
                    tenant.PrimaryAddress.ProvinceId = model.PrimaryAddress.Province.Id;
                    tenant.PrimaryAddress.StreetAddress = model.PrimaryAddress.StreetAddress;
                    tenant.PrimaryAddress.LastModifiedUserId = modifiedByUserId;
                    tenant.PrimaryAddress.LastModifiedUtc = DateTime.UtcNow;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.SetError($"An unexpected error occurred while saving the Tenant object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Tenant object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService.UpdateAsync - exception:{@Exception}", ex);

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
                var tenant = await _unitOfWork.TenantRepository.GetAsync(id);
                if (tenant != null)
                {
                    tenant.DeletedUtc = DateTime.UtcNow;
                    tenant.DeletedUserId = deletedByUserId;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.SetError($"An unexpected error occurred while removing the Tenant object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Tenant object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService.DeleteAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
