namespace HFP.Infrastructure.EF.Models
{
    internal class ProductReadModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ProductTransactionReadModel> ProductTransactions { get; set; }

    }
}
