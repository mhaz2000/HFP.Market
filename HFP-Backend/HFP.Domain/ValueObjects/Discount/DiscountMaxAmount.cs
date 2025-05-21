using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Discount
{
    public class DiscountMaxAmount : ValueObject
    {
        public decimal Value { get; }

        public DiscountMaxAmount(decimal value)
        {
            if (value <= 1000)
                throw new BusinessException("سقف مبلغ تحفیف باید بزرگتر از 1000 تومان باشد.");

            Value = value;
        }

        public static DiscountMaxAmount Create(decimal maxAmout) => new DiscountMaxAmount(maxAmout);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }


        public static implicit operator decimal(DiscountMaxAmount maxAmout)
            => maxAmout.Value;

        public static implicit operator DiscountMaxAmount(int maxAmout)
            => new(maxAmout);

    }
}
