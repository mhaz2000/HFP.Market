using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Discount
{
    public class DiscountUsageLimitPerUser : ValueObject
    {
        public int Value { get; }

        public DiscountUsageLimitPerUser(int value)
        {
            if (value < 0)
                throw new BusinessException("تعداد دفعات تکرار کد تخفیف باید بیشتر از 0 باشد.");

            Value = value;
        }

        public static DiscountUsageLimitPerUser Create(int usageLimit) => new DiscountUsageLimitPerUser(usageLimit);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }


        public static implicit operator int(DiscountUsageLimitPerUser usageLimit)
            => usageLimit.Value;

        public static implicit operator DiscountUsageLimitPerUser(int usageLimit)
            => new(usageLimit);

    }
}
