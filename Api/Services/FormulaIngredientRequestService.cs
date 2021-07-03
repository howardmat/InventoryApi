using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FormulaIngredientRequestService
    {
        private readonly FormulaIngredientEntityService _formulaIngredientEntityService;

        public FormulaIngredientRequestService(
            FormulaIngredientEntityService formulaIngredientEntityService)
        {
            _formulaIngredientEntityService = formulaIngredientEntityService;
        }

        public async Task<ResponseHandler<FormulaIngredientModel>> ProcessCreateRequestAsync(FormulaIngredientRequest model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<FormulaIngredientModel>();

            response.Data = await _formulaIngredientEntityService.CreateAsync(model.FormulaId.Value, model.MaterialId.Value, model.Quantity.Value, createdByUserId);
            if (response.Data == null)
            {
                response.SetError($"An unexpected error occurred while saving the Formula Ingredient object");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var formulaIngredient = await _formulaIngredientEntityService.GetEntityOrDefaultAsync(id, tenantId);
            if (formulaIngredient != null)
            {
                // Try to update and set response
                if (!await _formulaIngredientEntityService.DeleteAsync(formulaIngredient, deletedByUserId))
                {
                    response.SetError($"An unexpected error occurred while removing the Formula Ingredient object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Formula Ingredient object ({id})");
            }

            return response;
        }
    }
}
