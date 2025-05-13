namespace HFP.Application.DTO
{
    public record ProductDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Image { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
    }
}
