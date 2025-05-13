using AutoMapper;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Models;

namespace HFP.Infrastructure.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductReadModel, ProductDto>();
        }
    }
}
