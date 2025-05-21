using HFP.Domain.Consts;
using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Discounts
{
    public record CreateDiscountCommand(string name, string code, decimal? maxAmount, int percentage,
        int usageLimitPerUser, string startDate, string endDate, DiscountType Type = DiscountType.General) : ICommand;
}
