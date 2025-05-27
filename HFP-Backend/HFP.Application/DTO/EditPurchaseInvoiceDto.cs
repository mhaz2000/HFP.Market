namespace HFP.Application.DTO
{
    public record EditPurchaseInvoiceDto
    {
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public required string Date { get; set; }

        public IEnumerable<PurchaseInvoiceItemDto> Items { get; set; }
    }
}
