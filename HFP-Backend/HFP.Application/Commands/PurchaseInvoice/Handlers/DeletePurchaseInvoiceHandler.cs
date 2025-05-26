using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.PurchaseInvoice.Handlers
{
    internal class DeletePurchaseInvoiceHandler : ICommandHandler<DeletePurchaseInvoiceCommand>
    {
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;

        public DeletePurchaseInvoiceHandler(IPurchaseInvoiceRepository purchaseInvoiceRepository)
        {
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
        }
        public async Task Handle(DeletePurchaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var purchaseInvoice = await _purchaseInvoiceRepository.GetAsync(p => p.Id == request.Id, p => p.Items);
            if (purchaseInvoice is null)
                throw new BusinessException("فاکتور خرید یافت نشد.");

            await _purchaseInvoiceRepository.DeleteAsync(purchaseInvoice.Id);
        }
    }
}
