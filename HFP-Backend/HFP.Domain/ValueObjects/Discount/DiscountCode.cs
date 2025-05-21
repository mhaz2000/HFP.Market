using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;
using System.Text.RegularExpressions;

namespace HFP.Domain.ValueObjects.Discount
{
    public class DiscountCode : ValueObject
    {
        public string Value { get; }

        public DiscountCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("کد تخفیف اجباری است.");

            if (value.Length < 4 || value.Length > 10)
                throw new BusinessException("کد تخفیف باید بین 4 تا 10 کاراکتر باشد.");

            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                throw new BusinessException("کد تخفیف فقط می‌تواند شامل اعداد و حروف انگلیسی باشد.");

            Value = value;
        }

        public static DiscountCode Create(string code) => new DiscountCode(code);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(DiscountCode code)
            => code.Value;

        public static implicit operator DiscountCode(string code)
            => new(code);

    }
}
