using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;
using HFP.Shared.Helpers;

namespace HFP.Application.Commands.PurchaseInvoice.Handlers
{
    internal class UpdatePurchaseInvoiceHandler : ICommandHandler<UpdatePurchaseInvoiceCommand>
    {
        private readonly IPurchaseInvoiceFactory _purchaseInvoiceFactory;
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;

        public UpdatePurchaseInvoiceHandler(IPurchaseInvoiceFactory purchaseInvoiceFactory, IPurchaseInvoiceRepository purchaseInvoiceRepository)
        {
            _purchaseInvoiceFactory = purchaseInvoiceFactory;
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
        }
        public async Task Handle(UpdatePurchaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            var purchaseInvoice = await _purchaseInvoiceRepository.GetAsync(p => p.Id == request.Id, p => p.Items);
            if (purchaseInvoice is null)
                throw new BusinessException("فاکتور خرید یافت نشد.");

            _purchaseInvoiceFactory.Update(request.ImageId, request.Date.ToDate(false), purchaseInvoice);

            foreach (var item in purchaseInvoice.Items)
                _purchaseInvoiceRepository.DeleteItem(item);


            foreach (var item in request.Items)
            {
                var purchaseInvoiceItem = _purchaseInvoiceFactory.Create(item.ProductName, item.Quantity, item.PurchasePrice, purchaseInvoice);
                await _purchaseInvoiceRepository.AddItemAsync(purchaseInvoiceItem);
            }

            await _purchaseInvoiceRepository.UpdateAsync(purchaseInvoice);
        }
    }
}
