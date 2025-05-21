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

            CreateMap<ProductTransaction, ProductTransactionDto>()
                .ConstructUsing(tr => new ProductTransactionDto()
                {
                    Price = tr.Product.Price,
                    ProductName = tr.Product.Name,
                    Quantity = tr.Quantity
                });

            CreateMap<TransactionReadModel, TransactionDto>()
                .ConstructUsing(tr => new TransactionDto()
                {
                    BuyerId = tr.BuyerId,
                    DateTime = $"{pc.GetYear(tr.Date):0000}/{pc.GetMonth(tr.Date):00}/{pc.GetDayOfMonth(tr.Date):00} - {pc.GetHour(tr.Date):00}:{pc.GetMinute(tr.Date):00}",
                    Price = tr.ProductTransactions.Sum(p=> p.Quantity * p.Product.Price),
                    TransactionId = tr.Id
                });

        }

    }
}
