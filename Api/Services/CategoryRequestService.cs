using Api.Models;
using Data.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CategoryRequestService
    {
        private readonly ILogger<CategoryRequestService> _logger;
        private readonly CategoryEntityService _categoryEntityService;

        public CategoryRequestService(
            ILogger<CategoryRequestService> logger,
            CategoryEntityService categoryEntityService)
        {
            _logger = logger;
            _categoryEntityService = categoryEntityService;
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> ProcessListRequestAsync(CategoryType categoryType)
        {
            var response = new ServiceResponse<IEnumerable<CategoryModel>>();

            try
            {
                response.Data = await _categoryEntityService.ListAsync(categoryType);
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryRequestService.GetResponseForGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<CategoryModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ServiceResponse<CategoryModel>();

            try
            {
                response.Data = await _categoryEntityService.GetModelOrDefaultAsync(id);
                if (response.Data == null)
                {
                    response.SetNotFound($"Unable to locate Category object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryRequestService.GetResponseForGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<CategoryModel>> ProcessCreateRequestAsync(CategoryModel model, CategoryType categoryType, int createdByUserId, int tenantId)
        {
            var response = new ServiceResponse<CategoryModel>();

            try
            {
                response.Data = await _categoryEntityService.CreateAsync(model.Name, categoryType, createdByUserId, tenantId);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while saving the Category object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryRequestService.GetResponseForCreateRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse> ProcessUpdateRequestAsync(int id, CategoryModel model, int modifiedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                // Fetch the existing object
                var category = await _categoryEntityService.GetEntityOrDefaultAsync(id);
                if (category != null)
                {
                    // Try to update and set response
                    if (!await _categoryEntityService.UpdateAsync(category, model.Name, modifiedByUserId))
                    {
                        response.SetError("An unexpected error occurred while saving the Category object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Category object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryRequestService.GetResponseForUpdateRequestAsync - exception:{@Exception}", ex);

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
                var category = await _categoryEntityService.GetEntityOrDefaultAsync(id);
                if (category != null)
                {
                    // Try to update and set response
                    if (!await _categoryEntityService.DeleteAsync(category, deletedByUserId))
                    {
                        response.SetError("An unexpected error occurred while removing the Category object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Category object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryRequestService.GetResponseForDeleteRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
