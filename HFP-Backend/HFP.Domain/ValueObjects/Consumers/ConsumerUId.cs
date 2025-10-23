using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Consumers
{
    public class ConsumerUId : ValueObject
    {
        public string Value { get; }

        private ConsumerUId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("شناسه مصرف کننده اجباری است.");

            Value = value;
        }

        public static ConsumerUId Create(string consumerUId) => new ConsumerUId(consumerUId);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(ConsumerUId consumerUId)
            => consumerUId.Value;

        public static implicit operator ConsumerUId(string consumerUId)
            => new(consumerUId);
    }
}
