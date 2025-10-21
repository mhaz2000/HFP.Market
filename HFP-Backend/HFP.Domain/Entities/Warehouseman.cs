using HFP.Domain.ValueObjects.Warehouseman;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class Warehouseman : AggregateRoot<Guid>
    {
        public WarehousemanName Name { get; set; }
        public WarehousemanUId UId { get; set; }
        public Warehouseman(WarehousemanName name, WarehousemanUId uId)
        {
            Name = name;
            UId = uId;
        }
    }
}
