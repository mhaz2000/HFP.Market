using HFP.Domain.Consts;

namespace HFP.Infrastructure.EF.Models
{
    internal class DiscountReadModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public int Percentage { get; set; }
        public decimal? MaxAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsageLimitPerUser { get; set; }
        public DiscountType Type { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<DiscountBuyerReadModel> DiscountBuyers { get; set; }
    }
}
