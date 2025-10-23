using HFP.Domain.ValueObjects.Transactinos;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class Buyer : AggregateRoot<Guid>
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public BuyerId BuyerId { get; private set; }


        private readonly List<DiscountBuyer> discounts = new List<DiscountBuyer>();
        public IReadOnlyCollection<DiscountBuyer> Discounts => discounts.AsReadOnly();

        public Buyer(string? firstName, string? lastName, BuyerId buyerId)
        {
            FirstName = firstName;
            LastName = lastName;
            BuyerId = buyerId;
        }
    }
}
