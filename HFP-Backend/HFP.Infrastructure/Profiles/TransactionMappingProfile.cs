using AutoMapper;
using HFP.Application.DTO;
using HFP.Domain.Entities;
using HFP.Infrastructure.EF.Models;
using System;
using System.Globalization;

namespace HFP.Infrastructure.Profiles
{
    internal class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            var pc = new PersianCalendar();
            var iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");

            CreateMap<ProductTransaction, ProductTransactionDto>()
                .ConstructUsing(tr => new ProductTransactionDto()
                {
                    Price = tr.Product.Price,
                    ProductName = tr.Product.Name,
                    Quantity = tr.Quantity,
                    ProductCode = tr.Product.Code
                });

            CreateMap<TransactionReadModel, TransactionDto>()
                .ConstructUsing(tr => new TransactionDto()
                {
                    BuyerId = tr.BuyerId,
                    DateTime = $"{pc.GetYear(TimeZoneInfo.ConvertTimeFromUtc(tr.Date, iranTimeZone)):0000}/" +
                                   $"{pc.GetMonth(TimeZoneInfo.ConvertTimeFromUtc(tr.Date, iranTimeZone)):00}/" +
                                   $"{pc.GetDayOfMonth(TimeZoneInfo.ConvertTimeFromUtc(tr.Date, iranTimeZone)):00} - " +
                                   $"{pc.GetHour(TimeZoneInfo.ConvertTimeFromUtc(tr.Date, iranTimeZone)):00}:{pc.GetMinute(TimeZoneInfo.ConvertTimeFromUtc(tr.Date, iranTimeZone)):00}",
                    Price = tr.ProductTransactions.Sum(p => p.Quantity * p.Product.Price),
                    TransactionId = tr.Id
                });
        }

    }
}
