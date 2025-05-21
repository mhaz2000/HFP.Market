using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class DiscountBuyer : Entity<Guid>
    {
        public Guid DiscountId { get; set; }
        public Discount Discount { get; set; }

        public Guid BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public int UsageCount { get; set; }

        public DiscountBuyer()
        {
            
        }

        public DiscountBuyer(Discount discount, Buyer buyer)
        {
            Discount = discount;
            Buyer = buyer;
            UsageCount = 0;
        }
    }
}
