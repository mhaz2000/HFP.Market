namespace HFP.Infrastructure.EF.Models
{
    internal class BuyerReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public required string BuyerId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<DiscountBuyerReadModel> DiscountBuyers { get; set; }

    }
}
