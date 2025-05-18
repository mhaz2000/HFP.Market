namespace HFP.Application.DTO
{
    public record TransactionDto
    {
        public string BuyerId { get; set; }
        public string DateTime { get; set; }
        public decimal Price { get; set; }
        public Guid TransactionId { get; set; }
    }
}
