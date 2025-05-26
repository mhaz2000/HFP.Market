using AutoMapper;
using HFP.Application.DTO;
using HFP.Infrastructure.EF.Models;
using System.Globalization;

namespace HFP.Infrastructure.Profiles
{
    public class InvoicePurchaseMappingProfile : Profile
    {
        public InvoicePurchaseMappingProfile()
        {
            var pc = new PersianCalendar();

            CreateMap<PurchaseInvoiceReadModel, PurchaseInvoiceDto>()
                .ConstructUsing(t => new PurchaseInvoiceDto()
                {
                    Id = t.Id,
                    Price = t.Items.Sum(c => c.Price),
                    Date = $"{pc.GetYear(t.Date):0000}/{pc.GetMonth(t.Date):00}/{pc.GetDayOfMonth(t.Date):00}"
                });
        }
    }
}
