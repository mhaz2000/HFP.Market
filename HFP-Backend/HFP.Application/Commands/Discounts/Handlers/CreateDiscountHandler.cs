using HFP.Shared.Helpers;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;
using System.Globalization;

namespace HFP.Application.Commands.Discounts.Handlers
{
    internal class CreateDiscountHandler : ICommandHandler<CreateDiscountCommand>
    {
        private readonly IDiscountFactory _factory;
        private readonly IDiscountRepository _repository;

        public CreateDiscountHandler(IDiscountRepository repository, IDiscountFactory factory)
        {
            _factory = factory;
            _repository = repository;
        }
        public async Task Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {

            var prevDiscountWithSameCode = await _repository.GetAsync(d => d.Code == request.code && d.Date.EndDate > DateTime.Now);
            if (prevDiscountWithSameCode is not null)
                throw new BusinessException("کد تخفیف تکراری است.");

            var discount = _factory.Create(request.name, request.code, request.percentage, request.maxAmount,
                request.startDate.ToDate(true), request.endDate.ToDate(false), request.usageLimitPerUser, request.Type);

            await _repository.AddAsync(discount);
        }
    }
}
