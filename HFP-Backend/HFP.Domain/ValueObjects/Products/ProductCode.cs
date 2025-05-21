using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Products
{
    public class ProductCode : ValueObject
    {
        public string Value { get; }

        private ProductCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("کد محصول اجباری است.");

            Value = value;
        }
        public static ProductCode Create(string productCode) => new ProductCode(productCode);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public static implicit operator string(ProductCode productCode)
            => productCode.Value;

        public static implicit operator ProductCode(string productCode)
            => new(productCode);
    }
}
