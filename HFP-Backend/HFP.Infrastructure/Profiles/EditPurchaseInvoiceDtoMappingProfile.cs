using AutoMapper;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Models;
using System.Globalization;

namespace HFP.Infrastructure.Profiles
{
    public class EditPurchaseInvoiceDtoMappingProfile : Profile
    {

        public EditPurchaseInvoiceDtoMappingProfile()
        {
            var pc = new PersianCalendar();

            CreateMap<PurchaseInvoiceItemReadModel, PurchaseInvoiceItemDto>()
                .ConstructUsing(i => new PurchaseInvoiceItemDto()
                {
                    ProductName = i.Name,
                    Quantity = i.Quantity,
                    PurchasePrice = i.Price
                });


            CreateMap<PurchaseInvoiceReadModel, EditPurchaseInvoiceDto>()
                .ConstructUsing(d => new EditPurchaseInvoiceDto()
                {
                    Id = d.Id,
                    ImageId = d.Image,
                    Items = d.Items.Select(i => new PurchaseInvoiceItemDto()
                    {
                        ProductName = i.Name,
                        Quantity = i.Quantity,
                        PurchasePrice = i.Price
                    }),
                    Date = d.Date.ToShortDateString()
                }).ForMember(dest => dest.Date, opt => opt.MapFrom(t => $"{pc.GetYear(t.Date):0000}/{pc.GetMonth(t.Date):00}/{pc.GetDayOfMonth(t.Date):00}"));
        }
    }
}
