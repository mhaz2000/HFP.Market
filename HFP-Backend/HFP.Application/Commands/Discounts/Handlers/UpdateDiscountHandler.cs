using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Discounts.Handlers
{
    internal class UpdateDiscountHandler : ICommandHandler<UpdateDiscountCommand>
    {
        private readonly IDiscountFactory _factory;
        private readonly IDiscountRepository _repository;

        public UpdateDiscountHandler(IDiscountRepository repository, IDiscountFactory factory)
        {
            _factory = factory;
            _repository = repository;
        }
        public async Task Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var prevDiscountWithSameCode = await _repository.GetAsync(d => d.Id != request.Id && d.Code == request.code && d.Date.EndDate > DateTime.UtcNow);
            if (prevDiscountWithSameCode is not null)
                throw new BusinessException("کد تخفیف تکراری است.");

            var discount = await _repository.GetAsync(d => d.Id == request.Id && d.Code == request.code);
            if (discount is null)
                throw new BusinessException("کد تخفیف یافت نشد.");

            var updatedDiscount = _factory.Update(request.name, request.code, request.percentage, request.maxAmount,
                request.startDate, request.endDate, request.usageLimitPerUser, request.Type, discount);

            await _repository.UpdateAsync(updatedDiscount);
        }
    }
}
