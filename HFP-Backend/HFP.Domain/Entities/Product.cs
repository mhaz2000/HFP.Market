using HFP.Domain.ValueObjects.Products;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class Product : AggregateRoot<Guid>
    {
        public ProductName Name { get; private set; }
        public ProductQuantity Quantity { get; private set; }
        public ProductPrice Price { get; private set; }
        public ProductCode Code { get; private set; }
        public ProductPrice PurchasePrice { get; private set; }
        public Guid? Image { get; private set; }

        public Guid? ShelfId { get; set; }
        public Shelf? Shelf { get; set; }

        private readonly List<ProductTransaction> _transactions = new List<ProductTransaction>();
        public IReadOnlyCollection<ProductTransaction> Transactions => _transactions.AsReadOnly();

        public Product(ProductName name, ProductCode code, ProductQuantity quantity, ProductPrice price, ProductPrice purchasePrice, Guid? image)
        {
            Id = Guid.NewGuid();
            Name = name;
            Quantity = quantity;
            Price = price;
            Code = code;
            Image = image;
            PurchasePrice = purchasePrice;
        }

        internal void Update(ProductName name, ProductCode code, ProductQuantity quantity, ProductPrice price, ProductPrice purchasePrice, Guid? image)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Image = image;
            Code = code;
            PurchasePrice = purchasePrice;
        }
    }
}
