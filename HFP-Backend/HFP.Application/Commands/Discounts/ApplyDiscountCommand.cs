using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Discounts
{
    public record ApplyDiscountCommand(string BuyerId, string Code): ICommand;
}
