namespace HFP.Application.DTO
{
    public record DashboardDto
    {
        public int TotalProducts { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
