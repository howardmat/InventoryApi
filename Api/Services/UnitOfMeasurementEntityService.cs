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
    public class UnitOfMeasurementEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitOfMeasurementEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitOfMeasurementModel>> ListAsync()
        {
            // Fetch data
            var data = await _unitOfWork.UnitOfMeasurementRepository.ListAsync();

            // Add to collection
            var list = new List<UnitOfMeasurementModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<UnitOfMeasurementModel>(item));
            }

            return list;
        }

        public async Task<UnitOfMeasurement> GetEntityOrDefaultAsync(int id)
        {
            // Fetch object
            var entity = await _unitOfWork.UnitOfMeasurementRepository.GetAsync(id);

            return entity;
        }

        public async Task<UnitOfMeasurementModel> GetModelOrDefaultAsync(int id)
        {
            UnitOfMeasurementModel model = null;

            // Fetch object
            var unitOfMeasurement = await GetEntityOrDefaultAsync(id);
            if (unitOfMeasurement != null)
            {
                model = _mapper.Map<UnitOfMeasurementModel>(unitOfMeasurement);
            }

            return model;
        }

        public async Task<UnitOfMeasurementModel> CreateAsync(
            string name, 
            string abbreviation, 
            MeasurementSystem measurementSystem, 
            MeasurementType measurementType, 
            int modifyingUserId)
        {
            UnitOfMeasurementModel model = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var unitOfMeasurement = new UnitOfMeasurement
            {
                Name = name,
                Abbreviation = abbreviation,
                System = measurementSystem,
                Type = measurementType,
                CreatedUserId = modifyingUserId,
                CreatedUtc = now,
                LastModifiedUserId = modifyingUserId,
                LastModifiedUtc = now
            };
            await _unitOfWork.UnitOfMeasurementRepository.AddAsync(unitOfMeasurement);

            if (await _unitOfWork.CompleteAsync() > 0)
            {
                model = _mapper.Map<UnitOfMeasurementModel>(unitOfMeasurement);
            }

            return model;
        }

        public async Task<bool> UpdateAsync(
            UnitOfMeasurement unitOfMeasurement, 
            string name, 
            string abbreviation, 
            int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            unitOfMeasurement.Name = name;
            unitOfMeasurement.Abbreviation = abbreviation;
            unitOfMeasurement.LastModifiedUserId = modifyingUserId;
            unitOfMeasurement.LastModifiedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteAsync(UnitOfMeasurement unitOfMeasurement, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            unitOfMeasurement.DeletedUserId = modifyingUserId;
            unitOfMeasurement.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
