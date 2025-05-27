namespace HFP.Application.DTO
{
    public record PurchaseInvoiceItemDto
    {
        public required string ProductName { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Quantity { get; set; }
    }
}
