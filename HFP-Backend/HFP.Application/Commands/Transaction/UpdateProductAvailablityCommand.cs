using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Transaction
{
    public record TakeProductsCommand : ICommand
    {
        public required string BuyerId { get; set; }
        public required IEnumerable<AvailableProduct> Products { get; set; }
    }

    public record PutProductsCommand : ICommand
    {
        public required string BuyerId { get; set; }
        public required IEnumerable<AvailableProduct> Products { get; set; }
    }

    public record AvailableProduct
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
    }
}
