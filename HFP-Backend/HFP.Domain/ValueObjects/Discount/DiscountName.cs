using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Discount
{
    public class DiscountName : ValueObject
    {
        public string Value { get; }

        public DiscountName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("عنوان تخفیف اجباری است.");

            Value = value;
        }

        public static DiscountName Create(string name) => new DiscountName(name);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(DiscountName name)
            => name.Value;

        public static implicit operator DiscountName(string name)
            => new(name);

    }
}
