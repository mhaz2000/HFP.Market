namespace HFP.Application.DTO
{
    public record PurchaseInvoiceDto
    {
        public Guid Id { get; set; }
        public required string Date { get; set; }
        public decimal Price { get; set; }
    }
}
