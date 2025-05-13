using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Products;

namespace HFP.Domain.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product Create(ProductName productName, ProductQuantity quantity, ProductPrice price, Guid? image)
        {
            var productNameValue = ProductName.Create(productName);
            var quantityValue = ProductQuantity.Create(quantity);
            var priceValue = ProductPrice.Create(price);

            return new Product(productName, quantity, priceValue, image);
        }

        public void Update(ProductName productName, ProductQuantity quantity, ProductPrice price, Guid? image, Product product)
        {
            var productNameValue = ProductName.Create(productName);
            var quantityValue = ProductQuantity.Create(quantity);
            var priceValue = ProductPrice.Create(price);

            product.Update(productNameValue, quantityValue, price, image);
        }

        public void UpdateQuantity(ProductQuantity quantity, Product product)
        {
            product.Update(product.Name, quantity, product.Price, product.Image);
        }
    }
}
