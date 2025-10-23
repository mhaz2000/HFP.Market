using HFP.Application.DTO;
using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Purchase
{
    public record AddProductToCartCommand(string ProductCode) : ICommand<string?>;
}
