using HFP.Domain.Consts;
using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Transactinos;

namespace HFP.Domain.Factories.interfaces
{
    public interface ITransactionFactory
    {
        Transaction Create(TransactionStatus status, TransactionType type, DateTime date, BuyerId buyerId);
    }
}
