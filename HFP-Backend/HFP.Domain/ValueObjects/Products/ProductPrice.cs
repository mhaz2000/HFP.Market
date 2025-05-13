using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Products
{
    public class ProductPrice : ValueObject
    {
        public decimal Value { get; }

        private ProductPrice(decimal value)
        {
            if (value < 0)
                throw new BusinessException("قیمت محصول نمی‌تواند منفی باشد.");

            Value = value;
        }
        public static ProductPrice Create(decimal productPrice) => new ProductPrice(productPrice);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator decimal(ProductPrice productPrice)
            => productPrice.Value;

        public static implicit operator ProductPrice(decimal productPrice)
            => new(productPrice);
    }
}
