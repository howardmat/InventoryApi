using Api.Models.Dto;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialModel>> ListAsync()
        {
            // Fetch data
            var data = await _unitOfWork.MaterialRepository.ListAsync();

            // Add to collection
            var list = new List<MaterialModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<MaterialModel>(item));
            }

            return list;
        }

        public async Task<Material> GetEntityOrDefaultAsync(int id)
        {
            // Fetch object
            var entity = await _unitOfWork.MaterialRepository.GetAsync(id);

            return entity;
        }

        public async Task<MaterialModel> GetModelOrDefaultAsync(int id)
        {
            MaterialModel model = null;

            // Fetch object
            var material = await _unitOfWork.MaterialRepository.GetAsync(id);

            // Set response
            if (material != null)
            {
                model = _mapper.Map<MaterialModel>(material);
            }

            return model;
        }

        public async Task<MaterialModel> CreateAsync(MaterialModel model, int modifyingUserId, int tenantId)
        {
            MaterialModel newModel = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var material = new Material
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
            await _unitOfWork.MaterialRepository.AddAsync(material);

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                newModel = _mapper.Map<MaterialModel>(material);
            }

            return newModel;
        }

        public async Task<bool> UpdateAsync(Material material, MaterialModel model, int modifyingUserId)
        {
            // Update properties
            material.Name = model.Name;
            material.CategoryId = model.CategoryId.Value;
            material.Description = model.Description;
            material.UnitOfMeasurementId = model.UnitOfMeasurementId.Value;
            material.LastModifiedUserId = modifyingUserId;
            material.LastModifiedUtc = DateTime.UtcNow;

            // Set response
            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteAsync(Material material, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            material.DeletedUserId = modifyingUserId;
            material.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
