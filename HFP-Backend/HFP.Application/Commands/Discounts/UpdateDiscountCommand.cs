using HFP.Domain.Consts;
using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Discounts
{
    public record UpdateDiscountCommand(Guid Id ,string name, string code, decimal? maxAmount, int percentage,
        int usageLimitPerUser, DateTime startDate, DateTime endDate, DiscountType Type = DiscountType.General) : ICommand;
}
