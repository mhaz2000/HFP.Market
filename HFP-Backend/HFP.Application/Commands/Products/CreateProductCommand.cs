using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Products
{
    public record CreateProductCommand(Guid? ImageId, string Name, string code, int Quantity, decimal Price, decimal PurchasePrice) : ICommand;
}
