using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<Material, MaterialModel>();
        }
    }
}
