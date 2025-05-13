using AutoMapper;
using HFP.Application.DTO;
using HFP.Domain.Entities;

namespace HFP.Infrastructure.Profiles
{
    internal class TransactionMappingProfile : Profile
    {
        public TransactionMappingProfile()
        {
            CreateMap<ProductTransaction, ProductTransactionDto>()
                .ConstructUsing(tr => new ProductTransactionDto()
                {
                    Price = tr.Product.Price,
                    ProductName = tr.Product.Name,
                    Quantity = tr.Quantity
                });

        }

    }
}
