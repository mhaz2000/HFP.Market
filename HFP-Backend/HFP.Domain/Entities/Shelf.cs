using HFP.Domain.ValueObjects.Shelves;
using HFP.Shared.Abstractions.Domain;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.Entities
{
    public class Shelf : AggregateRoot<Guid>
    {
        public ShelfOrder Order { get; private set; }
        private readonly List<Product> _products = new List<Product>();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public Shelf(ShelfOrder order)
        {
            Order = order;
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            _products.AddRange(products);
        }
    }
}
