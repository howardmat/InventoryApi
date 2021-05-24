using Api.Models;
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

        public async Task<IEnumerable<CategoryModel>> ListAsync(CategoryType categoryType)
        {
            // Fetch data
            var data = await _unitOfWork.CategoryRepository.ListAsync(categoryType);

            // Add to collection
            var list = new List<CategoryModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<CategoryModel>(item));
            }

            return list;
        }

        public async Task<Category> GetEntityOrDefaultAsync(int id)
        {
            // Fetch object
            var entity = await _unitOfWork.CategoryRepository.GetAsync(id);

            return entity;
        }

        public async Task<CategoryModel> GetModelOrDefaultAsync(int id)
        {
            CategoryModel model = null;

            // Fetch object
            var category = await GetEntityOrDefaultAsync(id);
            if (category != null)
            {
                model = _mapper.Map<CategoryModel>(category);
            }

            return model;
        }

        public async Task<CategoryModel> CreateAsync(string name, CategoryType categoryType, int modifyingUserId)
        {
            CategoryModel model = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var category = new Category
            {
                Name = name,
                Type = categoryType,
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
