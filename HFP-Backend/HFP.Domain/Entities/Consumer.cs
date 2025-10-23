using HFP.Domain.ValueObjects.Consumers;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class Consumer : AggregateRoot<Guid>
    {
        public ConsumerName Name { get; private set; }
        public ConsumerUId UId { get; private set; }
        public bool IsWarehouseman { get; set; }
        public Consumer(ConsumerName name, ConsumerUId uId, bool isWarehouseman)
        {
            Name = name;
            UId = uId;
            IsWarehouseman = isWarehouseman;
        }
    }
}
