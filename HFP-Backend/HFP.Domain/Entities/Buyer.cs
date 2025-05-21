using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class Buyer : AggregateRoot<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }



        private readonly List<DiscountBuyer> discounts = new List<DiscountBuyer>();
        public IReadOnlyCollection<DiscountBuyer> Discounts => discounts.AsReadOnly();

        public Buyer(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
