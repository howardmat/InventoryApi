﻿using Api.Models;
using Api.Models.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialRequestService
    {
        private readonly ILogger<MaterialRequestService> _logger;
        private readonly MaterialEntityService _materialEntityService;
        public MaterialRequestService(
            ILogger<MaterialRequestService> logger,
            MaterialEntityService materialEntityService)
        {
            _logger = logger;
            _materialEntityService = materialEntityService;
        }

        public async Task<ServiceResponse<IEnumerable<MaterialModel>>> ProcessListRequestAsync()
        {
            var response = new ServiceResponse<IEnumerable<MaterialModel>>();

            try
            {
                response.Data = await _materialEntityService.ListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialRequestService.ProcessListRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<MaterialModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ServiceResponse<MaterialModel>();

            try
            {
                response.Data = await _materialEntityService.GetModelOrDefaultAsync(id);
                if (response.Data == null)
                {
                    response.SetNotFound($"Unable to locate Material object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialRequestService.ProcessGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<MaterialModel>> ProcessCreateRequestAsync(MaterialModel model, int createdByUserId, int tenantId)
        {
            var response = new ServiceResponse<MaterialModel>();

            try
            {
                response.Data = await _materialEntityService.CreateAsync(model, createdByUserId, tenantId);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while saving the Material object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialRequestService.ProcessCreateRequestAsync - exception:{@Exception}", ex);

                response.SetException(ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse> ProcessUpdateRequestAsync(int id, MaterialModel model, int modifiedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var material = await _materialEntityService.GetEntityOrDefaultAsync(id);
                if (material != null)
                {
                    if (!await _materialEntityService.UpdateAsync(material, model, modifiedByUserId))
                    {
                        response.SetError("An unexpected error occurred while saving the Material object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Material object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialRequestService.ProcessUpdateRequestAsync - exception:{@Exception}", ex);

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
                var material = await _materialEntityService.GetEntityOrDefaultAsync(id);
                if (material != null)
                {
                    if (!await _materialEntityService.DeleteAsync(material, deletedByUserId))
                    {
                        response.SetError("An unexpected error occurred while removing the Material object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Material object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialRequestService.ProcessDeleteRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
