namespace HFP.Infrastructure.EF.Models
{
    internal class ProductTransactionReadModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Guid TransactionId { get; set; }

        public decimal BuyTimePirce { get; set; }
        public decimal BuyTimePurchasePirce { get; set; }
        public int Quantity { get; set; }
        public ProductReadModel Product { get; set; }
        public TransactionReadModel Transaction { get; set; }
    }
}
