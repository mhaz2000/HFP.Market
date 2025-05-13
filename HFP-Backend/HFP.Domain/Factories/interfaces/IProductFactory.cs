using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Products;

namespace HFP.Domain.Factories.interfaces
{
    public interface IProductFactory
    {
        Product Create(ProductName productName, ProductQuantity quantity, ProductPrice price, Guid? image);
        void Update(ProductName productName, ProductQuantity quantity, ProductPrice price, Guid? image, Product product);
        void UpdateQuantity(ProductQuantity quantity, Product product);

    }
}
