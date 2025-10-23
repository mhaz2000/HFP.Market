using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Consumers;

namespace HFP.Domain.Factories.interfaces
{
    public interface IConsumerFactory
    {
        Consumer Create(ConsumerName name, ConsumerUId uid, bool isWarehouseman);
    }
}
