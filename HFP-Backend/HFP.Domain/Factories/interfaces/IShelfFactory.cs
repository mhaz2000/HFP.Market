using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Shelves;

namespace HFP.Domain.Factories.interfaces
{
    public interface IShelfFactory
    {
        Shelf Create(ShelfOrder shelfOrder);

    }
}
