using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class FormulaIngredientProfile : Profile
    {
        public FormulaIngredientProfile()
        {
            CreateMap<FormulaIngredient, FormulaIngredientModel>();
        }
    }
}
