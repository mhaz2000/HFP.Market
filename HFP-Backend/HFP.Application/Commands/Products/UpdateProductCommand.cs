using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Products
{
    public record UpdateProductCommand(Guid? Id, Guid? ImageId, string Name, string code, int Quantity, decimal Price, decimal purchasePrice) : ICommand;
}
