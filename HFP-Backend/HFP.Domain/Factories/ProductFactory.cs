using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Products;

namespace HFP.Domain.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product Create(ProductName productName, ProductCode code, ProductQuantity quantity, ProductPrice price, ProductPrice purchasePrice, Guid? image)
        {
            var productNameValue = ProductName.Create(productName);
            var quantityValue = ProductQuantity.Create(quantity);
            var productCodeValue = ProductCode.Create(code);
            var priceValue = ProductPrice.Create(price);
            var purchasePriceValue = ProductPrice.Create(purchasePrice);

            return new Product(productName, productCodeValue, quantity, priceValue, purchasePrice, image);
        }

        public void Update(ProductName productName, ProductCode code, ProductQuantity quantity, ProductPrice price, ProductPrice purchasePrice, Guid? image, Product product)
        {
            var productNameValue = ProductName.Create(productName);
            var quantityValue = ProductQuantity.Create(quantity);
            var priceValue = ProductPrice.Create(price);
            var productCodeValue = ProductCode.Create(code);
            var purchasePriceValue = ProductPrice.Create(purchasePrice);

            product.Update(productNameValue, productCodeValue, quantityValue, price, purchasePrice, image);
        }

        public void UpdateQuantity(ProductQuantity quantity, Product product)
        {
            product.Update(product.Name,product.Code, quantity, product.Price, product.PurchasePrice, product.Image);
        }
    }
}
