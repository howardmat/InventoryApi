using Api.Models;
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
    public class CategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(
            ILogger<CategoryService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> ListAsync(CategoryType categoryType)
        {
            var response = new ServiceResponse<IEnumerable<CategoryModel>>();

            try
            {
                // Fetch data
                var data = await _unitOfWork.CategoryRepository.ListAsync(categoryType);

                // Add to collection
                var list = new List<CategoryModel>();
                foreach (var item in data)
                {
                    list.Add(_mapper.Map<CategoryModel>(item));
                }

                // Set response
                response.Data = list;
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryService.ListAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<CategoryModel>> GetAsync(int id)
        {
            var response = new ServiceResponse<CategoryModel>();

            try
            {
                // Fetch object
                var category = await _unitOfWork.CategoryRepository.GetAsync(id);

                // Set response
                if (category != null)
                {
                    response.Data = _mapper.Map<CategoryModel>(category);
                }
                else
                {
                    response.AddError($"Unable to locate Category object ({id})");
                    response.SetNotFound();
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
                // Build and add the new object
                var now = DateTime.UtcNow;
                var category = new Category
                {
                    Name = model.Name,
                    Type = categoryType,
                    CreatedUserId = createdByUserId,
                    CreatedUtc = now,
                    LastModifiedUserId = createdByUserId,
                    LastModifiedUtc = now
                };
                await _unitOfWork.CategoryRepository.AddAsync(category);

                // Set response
                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    response.Data = _mapper.Map<CategoryModel>(category);
                }
                else
                {
                    response.AddError($"An unexpected error occurred while saving the Category object");
                    response.SetError();
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
                var category = await _unitOfWork.CategoryRepository.GetAsync(id);
                if (category != null)
                {
                    // Update entity
                    category.Name = model.Name;
                    category.LastModifiedUserId = modifiedByUserId;
                    category.LastModifiedUtc = DateTime.UtcNow;

                    // Set response
                    if (!(await _unitOfWork.CompleteAsync() > 0))
                    {
                        response.AddError($"An unexpected error occurred while saving the Category object");
                        response.SetError();
                    }
                }
                else
                {
                    response.SetNotFound();
                    response.AddError($"Unable to locate Category object ({id})");
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
                        response.AddError($"An unexpected error occurred while removing the Category object");
                        response.SetError();
                    }
                }
                else
                {
                    response.SetNotFound();
                    response.AddError($"Unable to locate Category object ({id})");
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
