using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Discounts
{
    public record DeleteDiscountCommand(Guid Id) : ICommand;
}
