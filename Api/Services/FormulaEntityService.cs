using Api.Authorization;
using Api.Models.Dto;
using Api.Models.RequestModels;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FormulaEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;   

        public FormulaEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FormulaModel>> ListAsync(int tenantId)
        {
            // Fetch data
            var data = await _unitOfWork.FormulaRepository.ListAsync(tenantId);

            // Add to collection
            var list = new List<FormulaModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<FormulaModel>(item));
            }

            return list;
        }

        public async Task<Formula> GetEntityOrDefaultAsync(int id, int tenantId)
        {
            // Fetch object
            var entity = await _unitOfWork.FormulaRepository.GetAsync(id, tenantId);

            return entity;
        }

        public async Task<FormulaModel> GetModelOrDefaultAsync(int id, int tenantId)
        {
            FormulaModel model = null;

            // Fetch object
            var formula = await _unitOfWork.FormulaRepository.GetAsync(id, tenantId);

            // Set response
            if (formula != null)
            {
                model = _mapper.Map<FormulaModel>(formula);
            }

            return model;
        }

        public async Task<FormulaModel> CreateAsync(FormulaRequest model, int modifyingUserId, int tenantId)
        {
            FormulaModel newModel = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var formula = new Formula
            {
                Name = model.Name,
                CategoryId = model.CategoryId.Value,
                Description = model.Description,
                CreatedUserId = modifyingUserId,
                LastModifiedUserId = modifyingUserId,
                TenantId = tenantId,
                CreatedUtc = now,
                LastModifiedUtc = now
            };
            await _unitOfWork.FormulaRepository.AddAsync(formula);

            // Add Ingredients if included
            foreach (var ingredient in model.Ingredients)
            {
                var formulaIngredient = new FormulaIngredient
                {
                    Formula = formula,
                    MaterialId = ingredient.MaterialId.Value,
                    Quantity = ingredient.Quantity.Value,
                    CreatedUserId = modifyingUserId,
                    LastModifiedUserId = modifyingUserId,
                    CreatedUtc = now,
                    LastModifiedUtc = now
                };

                formula.Ingredients.Add(formulaIngredient);
            }

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                formula = await _unitOfWork.FormulaRepository.GetAsync(formula.Id, tenantId);

                newModel = _mapper.Map<FormulaModel>(formula);
            }

            return newModel;
        }

        public async Task<bool> UpdateAsync(Formula formula, FormulaRequest model, int modifyingUserId)
        {
            // Update properties
            formula.Name = model.Name;
            formula.CategoryId = model.CategoryId.Value;
            formula.Description = model.Description;
            formula.LastModifiedUserId = modifyingUserId;
            formula.LastModifiedUtc = DateTime.UtcNow;

            // Set response
            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteAsync(Formula formula, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            formula.DeletedUserId = modifyingUserId;
            formula.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
