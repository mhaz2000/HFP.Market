namespace HFP.Application.DTO
{
    public record ProfitReportDto
    {
        public required string ProductName { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal SoldQuantity { get; set; }
        public decimal Profit { get; set; }
    }
}
