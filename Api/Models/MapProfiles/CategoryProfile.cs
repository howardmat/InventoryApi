using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
        }
    }
}
