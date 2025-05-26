using HFP.Domain.Entities;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.ValueObjects.Products;
using HFP.Domain.ValueObjects.PurchaseInvoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFP.Domain.Factories
{
    public class PurchaseInvoiceFactory : IPurchaseInvoiceFactory
    {
        public PurchaseInvoice Create(Guid? imageId, PurchaseInvoiceDate date)
        {
            var dateValue = PurchaseInvoiceDate.Create(date);

            return new PurchaseInvoice(imageId, dateValue);
        }
        public PurchaseInvoiceItem Create(ProductName name, ProductQuantity quantity, ProductPrice price, PurchaseInvoice purchaseInvoice)
        {
            var productNameValue = ProductName.Create(name);
            var quantityValue = ProductQuantity.Create(quantity);
            var priceValue = ProductPrice.Create(price);

            return new PurchaseInvoiceItem(productNameValue, quantityValue, priceValue, purchaseInvoice);
        }

        public void Update(Guid? imageId, PurchaseInvoiceDate date, PurchaseInvoice purchaseInvoice)
        {
            var dateValue = PurchaseInvoiceDate.Create(date);

            purchaseInvoice.Update(imageId, dateValue);
        }

        public void Update(ProductName name, ProductQuantity quantity, ProductPrice price, PurchaseInvoiceItem purchaseInvoiceItem)
        {
            var productNameValue = ProductName.Create(name);
            var quantityValue = ProductQuantity.Create(quantity);
            var priceValue = ProductPrice.Create(price);

            purchaseInvoiceItem.Update(productNameValue, quantityValue, priceValue);
        }
    }
}
