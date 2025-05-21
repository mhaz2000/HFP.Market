using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Discounts.Handlers
{
    internal class DeleteDiscountHandler : ICommandHandler<DeleteDiscountCommand>
    {
        private readonly IDiscountRepository _repository;

        public DeleteDiscountHandler(IDiscountRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _repository.GetAsync(d => d.Id == request.Id);
            if (discount is null)
                throw new BusinessException("کد تخفیف یافت نشد.");

            await _repository.DeleteAsync(discount.Id);
        }
    }
}
