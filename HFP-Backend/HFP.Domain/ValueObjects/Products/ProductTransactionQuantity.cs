using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Products
{
    public class ProductTransactionQuantity : ValueObject
    {
        public int Value { get; }

        private ProductTransactionQuantity(int value)
        {
            if (value < 0)
                throw new BusinessException("تعداد محصولات نمی‌تواند منفی باشد.");

            Value = value;
        }
        public static ProductTransactionQuantity Create(int productTransactionQuantity) => new ProductTransactionQuantity(productTransactionQuantity);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(ProductTransactionQuantity productTransactionQuantity)
            => productTransactionQuantity.Value;

        public static implicit operator ProductTransactionQuantity(int productTransactionQuantity)
            => new(productTransactionQuantity);
    }
}
