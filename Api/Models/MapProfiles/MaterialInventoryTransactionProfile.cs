using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class MaterialInventoryTransactionProfile : Profile
    {
        public MaterialInventoryTransactionProfile()
        {
            CreateMap<MaterialInventoryTransaction, MaterialInventoryTransactionModel>();
        }
    }
}
