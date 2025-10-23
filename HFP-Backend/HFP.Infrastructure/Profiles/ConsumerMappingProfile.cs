using AutoMapper;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Models;

namespace HFP.Infrastructure.Profiles
{
    public class ConsumerMappingProfile : Profile
    {
        public ConsumerMappingProfile()
        {
            CreateMap<ConsumerReadModel, ConsumerDto>();
        }
    }
}
