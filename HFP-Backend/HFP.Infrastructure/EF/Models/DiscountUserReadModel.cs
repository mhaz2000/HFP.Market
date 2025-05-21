namespace HFP.Infrastructure.EF.Models
{
    internal class DiscountBuyerReadModel
    {

        public Guid Id { get; set; }

        public Guid DiscountId { get; set; }
        public Guid BuyerId { get; set; }

        public int UsageCount { get; set; }

        public DiscountReadModel Discount { get; set; }
        public BuyerReadModel Buyer { get; set; }
    }
}
