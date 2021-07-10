using Api.Models.Dto;
using Api.Models.RequestModels;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProductEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductModel>> ListAsync(int tenantId)
        {
            // Fetch data
            var data = await _unitOfWork.ProductRepository.ListAsync(tenantId);

            // Add to collection
            var list = new List<ProductModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<ProductModel>(item));
            }

            return list;
        }

        public async Task<Product> GetEntityOrDefaultAsync(int id, int tenantId)
        {
            // Fetch object
            var entity = await _unitOfWork.ProductRepository.GetAsync(id, tenantId);

            return entity;
        }

        public async Task<ProductModel> GetModelOrDefaultAsync(int id, int tenantId)
        {
            ProductModel model = null;

            // Fetch object
            var product = await _unitOfWork.ProductRepository.GetAsync(id, tenantId);

            // Set response
            if (product != null)
            {
                model = _mapper.Map<ProductModel>(product);
            }

            return model;
        }

        public async Task<ProductModel> CreateAsync(ProductRequest model, int modifyingUserId, int tenantId)
        {
            ProductModel newModel = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var product = new Product
            {
                Name = model.Name,
                CategoryId = model.CategoryId.Value,
                Description = model.Description,
                UnitOfMeasurementId = model.UnitOfMeasurementId.Value,
                CreatedUserId = modifyingUserId,
                LastModifiedUserId = modifyingUserId,
                TenantId = tenantId,
                CreatedUtc = now,
                LastModifiedUtc = now
            };
            await _unitOfWork.ProductRepository.AddAsync(product);

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                newModel = await GetModelOrDefaultAsync(product.Id, tenantId);
            }

            return newModel;
        }

        public async Task<bool> UpdateAsync(Product product, ProductRequest model, int modifyingUserId)
        {
            // Update properties
            product.Name = model.Name;
            product.CategoryId = model.CategoryId.Value;
            product.Description = model.Description;
            product.UnitOfMeasurementId = model.UnitOfMeasurementId.Value;
            product.LastModifiedUserId = modifyingUserId;
            product.LastModifiedUtc = DateTime.UtcNow;

            // Set response
            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteAsync(Product product, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            product.DeletedUserId = modifyingUserId;
            product.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
