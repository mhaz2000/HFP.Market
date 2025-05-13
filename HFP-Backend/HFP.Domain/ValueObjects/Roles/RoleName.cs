using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Roles
{
    public class RoleName : ValueObject
    {
        public string Value { get; }

        private RoleName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام نقش نمی‌تواند خالی باشد.");

            Value = value;
        }

        public static RoleName Create(string roleName) => new RoleName(roleName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(RoleName roleName)
            => roleName.Value;

        public static implicit operator RoleName(string roleName)
            => new(roleName);
    }

}
