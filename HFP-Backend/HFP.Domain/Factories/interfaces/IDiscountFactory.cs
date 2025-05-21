using HFP.Domain.Consts;
using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Discount;

namespace HFP.Domain.Factories.interfaces
{
    public interface IDiscountFactory
    {
        Discount Create(DiscountName name, DiscountCode code, DiscountPercentage percentage, decimal? maxAmount, DateTime startDate,
            DateTime endDate, DiscountUsageLimitPerUser usageLimitPerUser, DiscountType type);

        Discount Update(DiscountName name, DiscountCode code, DiscountPercentage percentage, decimal? maxAmount, DateTime startDate,
            DateTime endDate, DiscountUsageLimitPerUser usageLimitPerUser, DiscountType type, Discount discount);

        DiscountBuyer Create(Discount discount, Buyer buyer);

    }
}