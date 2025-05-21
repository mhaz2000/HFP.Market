using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Discount
{
    public class DiscountPercentage : ValueObject
    {
        public int Value { get; }

        public DiscountPercentage(int value)
        {
            if (value < 0 || value >= 100)
                throw new BusinessException("درصد تخفیف باید بین 0 تا 100 باشد.");

            Value = value;
        }

        public static DiscountPercentage Create(int percentage) => new DiscountPercentage(percentage);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }


        public static implicit operator int(DiscountPercentage percentage)
            => percentage.Value;

        public static implicit operator DiscountPercentage(int percentage)
            => new(percentage);

    }
}
