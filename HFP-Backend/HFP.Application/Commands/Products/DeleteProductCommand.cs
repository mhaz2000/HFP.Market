using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Products
{
    public record DeleteProductCommand(Guid Id) : ICommand;
}
