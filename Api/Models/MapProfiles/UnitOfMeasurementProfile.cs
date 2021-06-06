using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class UnitOfMeasurementProfile : Profile
    {
        public UnitOfMeasurementProfile()
        {
            CreateMap<UnitOfMeasurement, UnitOfMeasurementModel>();
        }
    }
}
