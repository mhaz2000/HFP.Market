using HFP.Domain.Consts;
using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Transactinos;

namespace HFP.Domain.Factories
{
    public class TransactionFactory : ITransactionFactory
    {
        public Transaction Create(TransactionStatus status, TransactionType type, DateTime date, BuyerId buyerId)
        {
            var buyerIdValue = BuyerId.Create(buyerId);

            return new Transaction(status, type, buyerIdValue, date);
        }
    }
}
