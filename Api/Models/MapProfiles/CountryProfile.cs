using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryModel>();
        }
    }
}
