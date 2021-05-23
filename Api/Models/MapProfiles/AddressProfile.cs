using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressModel>();
        }
    }
}
