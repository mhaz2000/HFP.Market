using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Transactinos
{
    public class BuyerId : ValueObject
    {
        public string Value { get; }

        private BuyerId(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new BusinessException("شناسه خریدار نمی‌تواند خالی باشد.");

            Value = value;
        }
        public static BuyerId Create(string buyerId) => new BuyerId(buyerId);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(BuyerId buyerId)
            => buyerId.Value;

        public static implicit operator BuyerId(string buyerId)
            => new(buyerId);
    }
}
