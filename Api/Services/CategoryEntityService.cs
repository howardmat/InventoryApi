using Api.Models.Dto;
using AutoMapper;
using Data;
using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CategoryEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryModel>> ListAsync(CategoryType categoryType, int tenantId)
        {
            // Fetch data
            var data = await _unitOfWork.CategoryRepository.ListAsync(categoryType, tenantId);

            // Add to collection
            var list = new List<CategoryModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<CategoryModel>(item));
            }

            return list;
        }

        public async Task<Category> GetEntityOrDefaultAsync(int id, int tenantId)
        {
            // Fetch object
            var entity = await _unitOfWork.CategoryRepository.GetAsync(id, tenantId);

            return entity;
        }

        public async Task<CategoryModel> GetModelOrDefaultAsync(CategoryType categoryType, int id, int tenantId)
        {
            CategoryModel model = null;

            // Fetch object
            var category = await GetEntityOrDefaultAsync(id, tenantId);
            if (category != null && category.Type == categoryType)
            {
                model = _mapper.Map<CategoryModel>(category);
            }

            return model;
        }

        public async Task<CategoryModel> CreateAsync(string name, CategoryType categoryType, int modifyingUserId, int tenantId)
        {
            CategoryModel model = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var category = new Category
            {
                Name = name,
                Type = categoryType,
                TenantId = tenantId,
                CreatedUserId = modifyingUserId,
                CreatedUtc = now,
                LastModifiedUserId = modifyingUserId,
                LastModifiedUtc = now
            };
            await _unitOfWork.CategoryRepository.AddAsync(category);

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                model = _mapper.Map<CategoryModel>(category);
            }

            return model;
        }

        public async Task<bool> UpdateAsync(Category category, string name, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            category.Name = name;
            category.LastModifiedUserId = modifyingUserId;
            category.LastModifiedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteAsync(Category category, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            category.DeletedUserId = modifyingUserId;
            category.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
