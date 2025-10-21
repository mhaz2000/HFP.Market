using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Warehouseman
{
    public class WarehousemanName : ValueObject
    {
        public string Value { get; }

        private WarehousemanName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("نام انباردار اجباری است.");

            Value = value;
        }

        public static WarehousemanName Create(string warehousemanName) => new WarehousemanName(warehousemanName);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(WarehousemanName warehousemanName)
            => warehousemanName.Value;

        public static implicit operator WarehousemanName(string warehousemanName)
            => new(warehousemanName);
    }
}
