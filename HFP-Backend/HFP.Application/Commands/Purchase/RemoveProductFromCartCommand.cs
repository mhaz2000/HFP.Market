using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Purchase
{
    public record RemoveProductFromCartCommand(Guid ProductId, string BuyerId) : ICommand;
}
