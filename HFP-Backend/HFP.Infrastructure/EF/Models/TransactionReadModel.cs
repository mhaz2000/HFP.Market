using HFP.Domain.Consts;

namespace HFP.Infrastructure.EF.Models
{
    internal class TransactionReadModel
    {
        public Guid Id{ get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public required string BuyerId { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ProductTransactionReadModel> ProductTransactions { get; set; }

    }
}
