using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Consumers;

namespace HFP.Domain.Factories
{
    public class ConsumerFactory : IConsumerFactory
    {
        public Consumer Create(ConsumerName name, ConsumerUId uid, bool isWarehouseman)
        {
            var nameValue = ConsumerName.Create(name);
            var uidValue = ConsumerUId.Create(uid);

            return new Consumer(nameValue, uidValue, isWarehouseman);
        }
    }
}
