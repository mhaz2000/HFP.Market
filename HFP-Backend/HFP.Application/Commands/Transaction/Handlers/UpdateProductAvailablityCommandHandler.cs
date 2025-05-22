using HFP.Domain.Consts;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Transaction.Handlers
{
    internal class PutProductsHandler : ICommandHandler<PutProductsCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;

        public PutProductsHandler(IProductRepository productRepository, ITransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(PutProductsCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository
                .GetWithInclueAsync(tr => tr.BuyerId == request.BuyerId && tr.Type == TransactionType.PreInvoice && tr.Status == TransactionStatus.Pending);

            if (transaction is null)
                throw new BusinessException("تراکنش کاربر یافت نشد.");

            var updateDict = request.Products.ToDictionary(p => p.Code);
            var codes = updateDict.Keys.ToList();

            var products = await _productRepository.GetByCodesAsync(codes);


            foreach (var product in products)
                if (updateDict.TryGetValue(product.Code, out var updated))
                    transaction.RemoveProduct(product, updated.Quantity);

            if(transaction.Products.Any())
            {
                //Need to lock the door
            }


            await _transactionRepository.UpdateTransactionAsync(transaction);
        }
    }
    internal class TakeProductsHandler : ICommandHandler<TakeProductsCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TakeProductsHandler(IProductRepository productRepository, ITransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(TakeProductsCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository
                .GetWithInclueAsync(tr => tr.BuyerId == request.BuyerId && tr.Type == TransactionType.PreInvoice && tr.Status == TransactionStatus.Pending);

            if (transaction is null)
                throw new BusinessException("تراکنش کاربر یافت نشد.");

            var updateDict = request.Products.ToDictionary(p => p.Code);
            var codes = updateDict.Keys.ToList();

            var products = await _productRepository.GetByCodesAsync(codes);


            foreach (var product in products)
                if (updateDict.TryGetValue(product.Code, out var updated))
                    transaction.AddProduct(product, updated.Quantity);

            if(transaction.Products.Any())
            {
                //Need to lock the door
            }


            await _transactionRepository.UpdateTransactionAsync(transaction);
        }
    }
}
