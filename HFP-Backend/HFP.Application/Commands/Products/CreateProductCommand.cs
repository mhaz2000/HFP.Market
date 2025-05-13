using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Products
{
    public record CreateProductCommand(Guid? ImageId, string Name, int Quantity, decimal Price) : ICommand;
}
