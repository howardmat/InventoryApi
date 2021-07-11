using Api.Models.Dto;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FormulaIngredientEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FormulaIngredientEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FormulaIngredient> GetEntityOrDefaultAsync(int id, int tenantId)
        {
            // Fetch object
            var entity = await _unitOfWork.FormulaIngredientRepository.GetAsync(id, tenantId);

            return entity;
        }

        public async Task<FormulaIngredientModel> CreateAsync(int formulaId, int materialId, decimal quantity, int modifyingUserId)
        {
            FormulaIngredientModel model = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var formulaIngredient = new FormulaIngredient
            {
                FormulaId = formulaId,
                MaterialId = materialId,
                Quantity = quantity,
                CreatedUserId = modifyingUserId,
                CreatedUtc = now,
                LastModifiedUserId = modifyingUserId,
                LastModifiedUtc = now
            };
            await _unitOfWork.FormulaIngredientRepository.AddAsync(formulaIngredient);

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                model = _mapper.Map<FormulaIngredientModel>(formulaIngredient);
            }

            return model;
        }

        public async Task<bool> DeleteAsync(FormulaIngredient formulaIngredient, int modifyingUserId)
        {
            _unitOfWork.FormulaIngredientRepository.Remove(formulaIngredient);

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
