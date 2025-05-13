using HFP.Domain.Consts;
using HFP.Domain.ValueObjects.Products;
using HFP.Domain.ValueObjects.Transactinos;
using HFP.Shared.Abstractions.Domain;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.Entities
{
    public class Transaction : AggregateRoot<Guid>
    {
        public TransactionType Type { get; private set; }
        public TransactionStatus Status { get; private set; }
        public BuyerId BuyerId { get; private set; }
        public DateTime Date { get; private set; }

        private readonly List<ProductTransaction> _products = new List<ProductTransaction>();
        public IReadOnlyCollection<ProductTransaction> Products => _products.AsReadOnly();


        public Transaction(TransactionStatus status, TransactionType type, BuyerId buyerId, DateTime date)
        {
            Id = Guid.NewGuid();
            Status = status;
            BuyerId = buyerId;
            Date = date;
            Type = type;
        }

        public void UpdateProduct(Product product, int quantity)
        {
            if (product == null)
                throw new BusinessException("کالا یافت نشد.");

            if (Products.Any(r => r.ProductId == product.Id))
            {
                if (quantity > 0)
                    _products.FirstOrDefault(p => p.ProductId == product.Id)!.Quantity = quantity;
                else
                {
                    var productTransaction = _products.FirstOrDefault(r => r.ProductId == product.Id);
                    _products.Remove(productTransaction!);
                }
            }
            else if (quantity > 0)
                _products.Add(new ProductTransaction(product.Id, Id, product, this, quantity));
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new BusinessException("کالا یافت نشد.");

            if (Products.Any(r => r.ProductId == product.Id))
                _products.FirstOrDefault(r => r.ProductId == product.Id)!.Quantity++;
            else
                _products.Add(new ProductTransaction(product.Id, Id, product, this, 1));

        }
    }
}
