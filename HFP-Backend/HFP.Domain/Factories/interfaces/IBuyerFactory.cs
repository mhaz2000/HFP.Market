using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Transactinos;

namespace HFP.Domain.Factories.interfaces
{
    public interface IBuyerFactory
    {
        Buyer Create(BuyerId buyerId);

    }
}
