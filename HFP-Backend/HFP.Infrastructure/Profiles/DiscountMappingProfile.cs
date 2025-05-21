using AutoMapper;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Models;
using System.Globalization;

namespace HFP.Infrastructure.Profiles
{
    public class DiscountMappingProfile : Profile
    {

        public DiscountMappingProfile()
        {
            var pc = new PersianCalendar();

            CreateMap<DiscountReadModel, DiscountDto>()
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(d => $"{pc.GetYear(d.EndDate):0000}/{pc.GetMonth(d.EndDate):00}/{pc.GetDayOfMonth(d.EndDate):00}"))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(d => $"{pc.GetYear(d.StartDate):0000}/{pc.GetMonth(d.StartDate):00}/{pc.GetDayOfMonth(d.StartDate):00}"));
        }
    }
}
