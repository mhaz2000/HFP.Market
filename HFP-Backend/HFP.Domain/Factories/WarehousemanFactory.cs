using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Warehouseman;

namespace HFP.Domain.Factories
{
    public class WarehousemanFactory : IWarehousemanFactory
    {
        public Warehouseman Create(WarehousemanName name, WarehousemanUId uid)
        {
            var nameValue = WarehousemanName.Create(name);
            var uidValue = WarehousemanUId.Create(uid);

            return new Warehouseman(nameValue, uidValue);
        }
    }
}
