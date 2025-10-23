using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Consumers
{
    public class ConsumerName : ValueObject
    {
        public string Value { get; }

        private ConsumerName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام مصرف کننده اجباری است.");

            Value = value;
        }

        public static ConsumerName Create(string consumerName) => new ConsumerName(consumerName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(ConsumerName consumerName)
            => consumerName.Value;

        public static implicit operator ConsumerName(string consumerName)
            => new(consumerName);
    }
}
