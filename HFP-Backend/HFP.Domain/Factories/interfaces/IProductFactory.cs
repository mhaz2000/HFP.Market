using HFP.Domain.Entities;
using HFP.Domain.ValueObjects.Products;

namespace HFP.Domain.Factories.interfaces
{
    public interface IProductFactory
    {
        Product Create(ProductName productName, ProductCode code, ProductQuantity quantity, ProductPrice price, ProductPrice purchasePrice, Guid? image);
        void Update(ProductName productName, ProductCode code, ProductQuantity quantity, ProductPrice price, ProductPrice purchasePrice, Guid? image, Product product);
        void UpdateQuantity(ProductQuantity quantity, Product product);

    }
}