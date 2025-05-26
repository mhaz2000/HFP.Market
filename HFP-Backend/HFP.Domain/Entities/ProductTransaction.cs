using HFP.Domain.ValueObjects.Products;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class ProductTransaction : Entity<Guid>
    {
        public Guid TransactionId { get; private set; }
        public Guid ProductId { get; private set; }

        public Transaction Transaction { get; private set; }
        public Product Product { get; private set; }

        public decimal BuyTimePirce { get; set; }
        public decimal BuyTimePurchasePirce { get; set; }
        public ProductTransactionQuantity Quantity { get; set; }

        public ProductTransaction()
        {
            
        }
        public ProductTransaction(Guid productId, Guid transactionId, Product product, Transaction transaction,
            int quantity, decimal buyTimePrice, decimal buyTimePurchasePrice) : base(Guid.NewGuid())
        {
            ProductId = productId;
            TransactionId = transactionId;
            Quantity = ProductTransactionQuantity.Create(quantity);
            Product = product;
            Transaction = transaction;
            BuyTimePirce = buyTimePrice;
            BuyTimePurchasePirce = buyTimePurchasePrice;
        }
    }
}
