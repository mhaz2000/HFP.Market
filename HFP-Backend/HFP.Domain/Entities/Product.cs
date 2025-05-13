using HFP.Domain.ValueObjects.Products;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class Product : AggregateRoot<Guid>
    {
        public ProductName Name { get; private set; }
        public ProductQuantity Quantity { get; private set; }
        public ProductPrice Price { get; private set; }
        public Guid? Image { get; private set; }

        private readonly List<ProductTransaction> _transactions = new List<ProductTransaction>();
        public IReadOnlyCollection<ProductTransaction> Transactions => _transactions.AsReadOnly();

        public Product(ProductName name, ProductQuantity quantity, ProductPrice price, Guid? image)
        {
            Id = Guid.NewGuid();
            Name = name;
            Quantity = quantity;
            Price = price;
            Image = image;
        }

        internal void Update(ProductName name, ProductQuantity quantity, ProductPrice price, Guid? image)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Image = image;
        }
    }
}
