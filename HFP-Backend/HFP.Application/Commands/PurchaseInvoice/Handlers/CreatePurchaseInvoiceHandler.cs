using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.PurchaseInvoice.Handlers
{
    internal class CreatePurchaseInvoiceHandler : ICommandHandler<CreatePurchaseInvoiceCommand>
    {
        private readonly IPurchaseInvoiceFactory _purchaseInvoiceFactory;
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;

        public CreatePurchaseInvoiceHandler(IPurchaseInvoiceFactory purchaseInvoiceFactory, IPurchaseInvoiceRepository purchaseInvoiceRepository)
        {
            _purchaseInvoiceFactory = purchaseInvoiceFactory;
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
        }

        public async Task Handle(CreatePurchaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var (ImageId, Date, Items) = request;

            var purchaseInvoice = _purchaseInvoiceFactory.Create(ImageId, request.Date);
            await _purchaseInvoiceRepository.AddAsync(purchaseInvoice);

            foreach (var item in Items)
            {
                var purchaseInvoiceItem = _purchaseInvoiceFactory.Create(item.ProductName, item.Qunatity, item.PurchasePrice, purchaseInvoice);
                await _purchaseInvoiceRepository.AddItemAsync(purchaseInvoiceItem);
            }

            await _purchaseInvoiceRepository.CommitAsync();
        }
    }
}
