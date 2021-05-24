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
    public class TenantRequestService
    {
        private readonly ILogger<TenantRequestService> _logger;
        private readonly TenantEntityService _tenantEntityService;

        public TenantRequestService(
            ILogger<TenantRequestService> logger,
            TenantEntityService tenantEntityService)
        {
            _logger = logger;
            _tenantEntityService = tenantEntityService;
        }

        public async Task<ServiceResponse<IEnumerable<TenantModel>>> ProcessListRequestAsync()
        {
            var response = new ServiceResponse<IEnumerable<TenantModel>>();

            try
            {
                response.Data = await _tenantEntityService.ListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantRequestService.ProcessListRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<TenantModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ServiceResponse<TenantModel>();

            try
            {
                // Fetch object
                var tenant = await _tenantEntityService.GetModelOrDefaultAsync(id);
                if (tenant != null)
                {
                    response.Data = tenant;
                }
                else
                {
                    response.SetNotFound($"Unable to locate Tenant object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantRequestService.ProcessGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<TenantModel>> ProcessCreateRequestAsync(TenantModel model, int createdByUserId)
        {
            var response = new ServiceResponse<TenantModel>();

            try
            {
                var newTenant = await _tenantEntityService.CreateAsync(model, createdByUserId);
                if (newTenant != null)
                {
                    response.Data = newTenant;
                }
                else
                {
                    response.SetError($"An unexpected error occurred while saving the Tenant object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantRequestService.ProcessCreateRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse> ProcessUpdateRequestAsync(int id, TenantModel model, int modifiedByUserId)
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
                _logger.LogError("TenantRequestService.ProcessUpdateRequestAsync - exception:{@Exception}", ex);

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
                _logger.LogError("TenantRequestService.ProcessDeleteRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
