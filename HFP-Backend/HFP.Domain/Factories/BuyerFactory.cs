using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Transactinos;

namespace HFP.Domain.Factories
{
    public class BuyerFactory : IBuyerFactory
    {
        public Buyer Create(BuyerId buyerId)
        {
            var buyerIdValue = BuyerId.Create(buyerId);

            return new(string.Empty, string.Empty, buyerIdValue);
        }
    }
}
