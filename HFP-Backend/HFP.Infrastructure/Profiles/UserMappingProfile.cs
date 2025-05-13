using AutoMapper;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Models;

namespace HFP.Infrastructure.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserReadModel, UserDto>();
        }
    }
}
