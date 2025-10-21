using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.Warehouseman
{
    public class WarehousemanUId : ValueObject
    {
        public string Value { get; }

        private WarehousemanUId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BusinessException("شناسه انباردار اجباری است.");

            Value = value;
        }

        public static WarehousemanUId Create(string warehousemanUId) => new WarehousemanUId(warehousemanUId);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }


        public static implicit operator string(WarehousemanUId warehousemanUId)
            => warehousemanUId.Value;

        public static implicit operator WarehousemanUId(string warehousemanUId)
            => new(warehousemanUId);
    }
}
