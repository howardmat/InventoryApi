using Api.Models;
using Api.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                response.Data = await _tenantEntityService.GetModelOrDefaultAsync(id);
                if (response.Data == null)
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

        public async Task<ServiceResponse<TenantModel>> ProcessCreateRequestAsync(TenantPost model, int createdByUserId)
        {
            var response = new ServiceResponse<TenantModel>();

            try
            {
                var tenantModel = new TenantModel
                {
                    CompanyName = model.CompanyName,
                    PrimaryAddress = model.PrimaryAddress
                };

                response.Data = await _tenantEntityService.CreateAsync(tenantModel, createdByUserId);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while saving the Tenant object");
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
                var tenant = await _tenantEntityService.GetEntityOrDefaultAsync(id);
                if (tenant != null)
                {
                    if (!(await _tenantEntityService.UpdateAsync(tenant, model, modifiedByUserId)))
                    {
                        response.SetError("An unexpected error occurred while saving the Tenant object");
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
                var tenant = await _tenantEntityService.GetEntityOrDefaultAsync(id);
                if (tenant != null)
                {
                    if (!await _tenantEntityService.DeleteAsync(tenant, deletedByUserId))
                    {
                        response.SetError("An unexpected error occurred while removing the Tenant object");
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
