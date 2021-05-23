﻿using Api.Models;
using AutoMapper;
using Data;
using Data.Enums;
using Data.Models;
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

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> HandleGetAllAsync(CategoryType categoryType)
        {
            var response = new ServiceResponse<IEnumerable<CategoryModel>>();

            try
            {
                response.Data = await _categoryEntityService.ListAsync(categoryType);
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService.ListAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<CategoryModel>> HandleGetByIdAsync(int id)
        {
            var response = new ServiceResponse<CategoryModel>();

            try
            {
                var category = await _categoryEntityService.GetModelOrDefaultAsync(id);
                if (category != null)
                {
                    response.Data = category;
                }
                else
                {
                    response.SetNotFound($"Unable to locate Category object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService.GetAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<CategoryModel>> CreateAsync(CategoryModel model, CategoryType categoryType, int createdByUserId)
        {
            var response = new ServiceResponse<CategoryModel>();

            try
            {
                var newCategory = await _categoryEntityService.CreateAsync(model.Name, categoryType, createdByUserId);
                if (newCategory == null)
                {
                    response.SetError($"An unexpected error occurred while saving the Category object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService.CreateAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(int id, CategoryModel model, int modifiedByUserId)
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
                        response.SetError($"An unexpected error occurred while saving the Category object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Category object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService.UpdateAsync - exception:{@Exception}", ex);

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
                var category = await _unitOfWork.CategoryRepository.GetAsync(id);
                if (category != null)
                {
                    category.DeletedUtc = DateTime.UtcNow;
                    category.DeletedUserId = deletedByUserId;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.SetError($"An unexpected error occurred while removing the Category object");
                    }
                }
                else
                {
                    response.SetNotFound($"Unable to locate Category object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService.DeleteAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}