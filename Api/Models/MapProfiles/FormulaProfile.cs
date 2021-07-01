using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class FormulaProfile : Profile
    {
        public FormulaProfile()
        {
            CreateMap<Formula, FormulaModel>();
        }
    }
}
