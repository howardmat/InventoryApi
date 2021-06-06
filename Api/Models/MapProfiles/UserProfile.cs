using Api.Models.Dto;
using AutoMapper;

namespace Api.Models.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Data.Models.UserProfile, UserModel>();
        }
    }
}
