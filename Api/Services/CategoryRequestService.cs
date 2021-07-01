using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using Data.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CategoryRequestService
    {
        private readonly CategoryEntityService _categoryEntityService;

        public CategoryRequestService(
            CategoryEntityService categoryEntityService)
        {
            _categoryEntityService = categoryEntityService;
        }

        public async Task<ResponseHandler<IEnumerable<CategoryModel>>> ProcessListRequestAsync(CategoryType categoryType)
        {
            var response = new ResponseHandler<IEnumerable<CategoryModel>>();

            response.Data = await _categoryEntityService.ListAsync(categoryType);

            return response;
        }

        public async Task<ResponseHandler<CategoryModel>> ProcessGetRequestAsync(CategoryType requestCategoryType, int id)
        {
            var response = new ResponseHandler<CategoryModel>();

            var category = await _categoryEntityService.GetModelOrDefaultAsync(requestCategoryType, id);
            if (category != null)
            {
                response.Data = category;
            }
            else
            {
                response.SetNotFound($"Unable to locate {requestCategoryType} object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<CategoryModel>> ProcessCreateRequestAsync(CategoryType requestCategoryType, CategoryRequest model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<CategoryModel>();

            response.Data = await _categoryEntityService.CreateAsync(model.Name, requestCategoryType, createdByUserId, tenantId);
            if (response.Data == null)
            {
                response.SetError($"An unexpected error occurred while saving the {requestCategoryType} object");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessUpdateRequestAsync(CategoryType requestCategoryType, int id, CategoryRequest model, int modifiedByUserId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var category = await _categoryEntityService.GetEntityOrDefaultAsync(id);
            if (category != null && category.Type == requestCategoryType)
            {
                // Try to update and set response
                if (!await _categoryEntityService.UpdateAsync(category, model.Name, modifiedByUserId))
                {
                    response.SetError($"An unexpected error occurred while saving the {requestCategoryType} object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate {requestCategoryType} object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(CategoryType requestCategoryType, int id, int deletedByUserId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var category = await _categoryEntityService.GetEntityOrDefaultAsync(id);
            if (category != null && category.Type == requestCategoryType)
            {
                // Try to update and set response
                if (!await _categoryEntityService.DeleteAsync(category, deletedByUserId))
                {
                    response.SetError($"An unexpected error occurred while removing the {requestCategoryType} object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate {requestCategoryType} object ({id})");
            }

            return response;
        }
    }
}
