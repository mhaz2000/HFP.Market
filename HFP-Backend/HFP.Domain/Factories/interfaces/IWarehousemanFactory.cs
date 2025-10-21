using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Warehouseman;

namespace HFP.Domain.Factories.interfaces
{
    public interface IWarehousemanFactory
    {
        Warehouseman Create(WarehousemanName name, WarehousemanUId uid);
    }
}
