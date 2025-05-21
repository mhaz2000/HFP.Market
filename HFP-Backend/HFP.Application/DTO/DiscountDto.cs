using HFP.Domain.Consts;

namespace HFP.Application.DTO
{
    public class DiscountDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public int Percentage { get; set; }
        public decimal? MaxAmount { get; set; }
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
        public int UsageLimitPerUser { get; set; }
        public DiscountType Type { get; set; }
    }
}
