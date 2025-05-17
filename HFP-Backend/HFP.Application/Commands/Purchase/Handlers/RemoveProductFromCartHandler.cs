using HFP.Domain.Consts;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Purchase.Handlers
{
    internal class RemoveProductFromCartHandler : ICommandHandler<RemoveProductFromCartCommand>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IProductRepository _productRepository;

        public RemoveProductFromCartHandler(ITransactionRepository transactionRepository, IProductRepository productRepository)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
        }

        public async Task Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository
                .GetAsync(t => t.BuyerId == request.BuyerId && t.Type == TransactionType.Invoice && t.Status == TransactionStatus.Pending, t => t.Products);

            if (transaction is null)
                throw new BusinessException("فاکتور خرید یافت نشد.");


            var product = await _productRepository.GetAsync(p => p.Id == request.ProductId);
            if (product is null)
                throw new BusinessException("کالا مورد نظر یافت نشد.");

            transaction.RemoveProduct(product);

            await _transactionRepository.UpdateTransactionAsync(transaction);
        }
    }
}
