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
        public DateTime Date { get; set; }

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

        public void AddProduct(Product product, int quantity)
        {
            if (product == null)
                throw new BusinessException("کالا یافت نشد.");

            if (Products.Any(r => r.ProductId == product.Id))
                _products.FirstOrDefault(r => r.ProductId == product.Id)!.Quantity += quantity;
            else if (quantity > 0)
                _products.Add(new ProductTransaction(product.Id, Id, product, this, quantity, product.Price, product.PurchasePrice));
        }

        public void RemoveProduct(Product product, int quanity)
        {
            if (product == null)
                throw new BusinessException("کالا یافت نشد.");

            var productTransaction = Products.FirstOrDefault(r => r.ProductId == product.Id);

            if (productTransaction is null)
                throw new BusinessException("کالا در سبد خرید یافت نشد.");

            if (productTransaction.Quantity == 1)
                _products.Remove(productTransaction);
            else
                productTransaction.Quantity--;

        }
    }
}
