using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Products
{
    
    public class ProductName : ValueObject
    {
        public string Value { get; }

        private ProductName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام محصول اجباری است.");

            Value = value;
        }
        public static ProductName Create(string productName) => new ProductName(productName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public static implicit operator string(ProductName productName)
            => productName.Value;

        public static implicit operator ProductName(string productName)
            => new(productName);
    }
}
