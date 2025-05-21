using HFP.Domain.Consts;
using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Discount;

namespace HFP.Domain.Factories
{
    public class DiscountFactory : IDiscountFactory
    {
        public Discount Create(DiscountName name, DiscountCode code, DiscountPercentage percentage, decimal? maxAmount, DateTime startDate,
            DateTime endDate, DiscountUsageLimitPerUser usageLimitPerUser, DiscountType type)
        {
            var discountCodeValue = DiscountCode.Create(code);
            var discountNameValue = DiscountName.Create(name);
            var discountPercentageValue = DiscountPercentage.Create(percentage);
            var discountMaxAmountValue = maxAmount is not null ? DiscountMaxAmount.Create(maxAmount.Value) : null;
            var discountDateValue = DiscountDate.Create(startDate, endDate);
            var discountUsageLimitPerUserValue = DiscountUsageLimitPerUser.Create(usageLimitPerUser);

            return new Discount(discountNameValue, discountCodeValue, discountDateValue, discountMaxAmountValue, discountPercentageValue, discountUsageLimitPerUserValue, type);
        }

        public DiscountBuyer Create(Discount discount, Buyer buyer)
            => new DiscountBuyer(discount, buyer);

        public Discount Update(DiscountName name, DiscountCode code, DiscountPercentage percentage, decimal? maxAmount, DateTime startDate, DateTime endDate, DiscountUsageLimitPerUser usageLimitPerUser, DiscountType type, Discount discount)
        {
            var discountCodeValue = DiscountCode.Create(code);
            var discountNameValue = DiscountName.Create(name);
            var discountPercentageValue = DiscountPercentage.Create(percentage);
            var discountMaxAmountValue = maxAmount is not null ? DiscountMaxAmount.Create(maxAmount.Value) : null;
            var discountDateValue = DiscountDate.Create(startDate, endDate);
            var discountUsageLimitPerUserValue = DiscountUsageLimitPerUser.Create(usageLimitPerUser);

            discount.Update(discountNameValue, discountCodeValue, discountDateValue, discountMaxAmountValue, discountPercentageValue, discountUsageLimitPerUserValue,
                type);

            return discount;
        }
    }
}
