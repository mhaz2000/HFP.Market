using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Transaction
{
    public record UpdateProductAvailablityCommand : ICommand
    {
        public string BuyerId { get; set; }
        public required IEnumerable<AvailableProduct> Products { get; set; }
    }

    public record AvailableProduct
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
