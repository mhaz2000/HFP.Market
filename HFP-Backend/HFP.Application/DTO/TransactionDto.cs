namespace HFP.Application.DTO
{
    public record ProductTransactionDto
    {
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ProductImage { get; set; }
    }
}
