using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Products
{
    public class ProductQuantity : ValueObject
    {
        public int Value { get; }

        private ProductQuantity(int value)
        {
            if (value < 0)
                throw new BusinessException("موجودی محصول نمی‌تواند منفی باشد.");

            Value = value;
        }
        public static ProductQuantity Create(int productQuantity) => new ProductQuantity(productQuantity);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(ProductQuantity productQuantity)
            => productQuantity.Value;

        public static implicit operator ProductQuantity(int productQuantity)
            => new(productQuantity);
    }
}
