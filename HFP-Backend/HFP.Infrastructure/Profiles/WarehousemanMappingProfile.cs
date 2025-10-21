using AutoMapper;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Models;

namespace HFP.Infrastructure.Profiles
{
    public class WarehousemanMappingProfile : Profile
    {
        public WarehousemanMappingProfile()
        {
            CreateMap<WarehousemanReadModel, WarehousemanDto>();
        }
    }
}
