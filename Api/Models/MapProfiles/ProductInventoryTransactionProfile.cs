using Api.Models.Dto;
using AutoMapper;
using Data.Models;

namespace Api.Models.MapProfiles
{
    public class ProductInventoryTransactionProfile : Profile
    {
        public ProductInventoryTransactionProfile()
        {
            CreateMap<ProductInventoryTransaction, ProductInventoryTransactionModel>();
        }
    }
}
