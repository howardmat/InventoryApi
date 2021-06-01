using AutoMapper;
using Data.Models;

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
