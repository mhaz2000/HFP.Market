using HFP.Application.DTO;
using HFP.Domain.Consts;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFP.Application.Commands.Discounts.Handlers
{
    internal class ApplyDiscountHandler : ICommandHandler<ApplyDiscountCommand, AppliedDiscountDto>
    {
        private readonly IDiscountRepository _repository;
        private readonly ITransactionRepository _transactionRepository;

        public ApplyDiscountHandler(IDiscountRepository repository, ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _transactionRepository = transactionRepository;
        }

        public async Task<AppliedDiscountDto> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _repository.GetWithInclueAsync(t => !t.IsDeleted && t.Code == request.Code &&
                t.Date.StartDate < DateTime.Now && t.Date.EndDate > DateTime.Now);

            if (discount is null)
                throw new BusinessException("کد تخفیف نامعتبر است.");

            var usageByBuyer = discount.Buyers.FirstOrDefault(b => b.Buyer.BuyerId == request.BuyerId);
            if (usageByBuyer is not null && usageByBuyer.UsageCount >= discount.UsageLimitPerUser)
                    throw new BusinessException("دفعات مجاز استفاده از کد تخفیف به پایان رسیده است.");

            var transaction = await _transactionRepository
                .GetWithInclueAsync(t => t.BuyerId == request.BuyerId && t.Type == TransactionType.Invoice && t.Status == TransactionStatus.Pending);

            if (transaction is null)
                throw new BusinessException("فاکتور یافت نشد.");

            var price = transaction.Products.Sum(t => t.BuyTimePirce * t.Quantity);


            var discountedPrice = price * (1 - ((decimal)discount.Percentage.Value / 100));
            discountedPrice = discount.MaxAmount is not null ? Math.Min(price - discount.MaxAmount, discountedPrice) : discountedPrice;

            return new AppliedDiscountDto()
            {
                NewPrice = discountedPrice
            };
        }
    }
}
