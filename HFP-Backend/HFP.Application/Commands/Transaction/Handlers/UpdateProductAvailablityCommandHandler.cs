using HFP.Domain.Consts;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Transaction.Handlers
{
    internal class UpdateProductAvailablityCommandHandler : ICommandHandler<UpdateProductAvailablityCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;

        public UpdateProductAvailablityCommandHandler(IProductRepository productRepository, ITransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(UpdateProductAvailablityCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository
                .GetAsync(tr => tr.BuyerId == request.BuyerId && tr.Type == TransactionType.PreInvoice && tr.Status == TransactionStatus.Pending);

            if (transaction is null)
                throw new BusinessException("تراکنش کاربر یافت نشد.");

            var updateDict = request.Products.ToDictionary(p => p.Id);
            var ids = updateDict.Keys.ToList();

            var products = await _productRepository.GetByIdsAsync(ids);


            foreach (var product in products)
                if (updateDict.TryGetValue(product.Id, out var updated))
                    transaction.UpdateProduct(product, product.Quantity - updated.Quantity);

            if(transaction.Products.Any())
            {
                //Need to lock the door
            }


            await _transactionRepository.UpdateTransactionAsync(transaction);
        }
    }
}
