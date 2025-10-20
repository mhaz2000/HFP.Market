using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Shelves;

namespace HFP.Domain.Factories
{
    public class ShelfFactory : IShelfFactory
    {
        public Shelf Create(ShelfOrder shelfOrder)
        {
            var shelfOrderValue = ShelfOrder.Create(shelfOrder);

            return new(shelfOrderValue);
        }
    }
}
