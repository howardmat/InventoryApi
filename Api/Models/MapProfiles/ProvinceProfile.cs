using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile()
        {
            CreateMap<Province, ProvinceModel>();
        }
    }
}
