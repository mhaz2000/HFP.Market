﻿using HFP.Domain.Consts;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Purchase.Handlers
{
    internal class AddProductToCartHandler : ICommandHandler<AddProductToCartCommand, bool>
    {
        private readonly ITransactionFactory _transactionFactory;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IProductRepository _productRepository;

        public AddProductToCartHandler(ITransactionRepository transactionRepository, ITransactionFactory transactionFactory, IProductRepository productRepository)
        {
            _transactionFactory = transactionFactory;
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository
                .GetAsync(t => t.BuyerId == request.BuyerId && t.Type == TransactionType.Invoice && t.Status == TransactionStatus.Pending, t => t.Products);

            if (transaction is null)
            {
                transaction = _transactionFactory.Create(TransactionStatus.Pending, TransactionType.Invoice, DateTime.Now, request.BuyerId);
                await _transactionRepository.AddAsync(transaction);
            }

            var product = await _productRepository.GetAsync(p => p.Code == request.ProductCode);
            if (product is null || product.Quantity <= 0)
                throw new BusinessException("کالا مورد نظر یافت نشد.");


            transaction.AddProduct(product, 1);

            if (product.Quantity < transaction.Products.FirstOrDefault(p => p.Product.Code == request.ProductCode)!.Quantity)
                throw new BusinessException("موجودی کالای مورد نظر کافی نیست.");

            await _transactionRepository.UpdateTransactionAsync(transaction);

            if (transaction.Products.Any())
                return true;


            return false;
        }
    }
}
