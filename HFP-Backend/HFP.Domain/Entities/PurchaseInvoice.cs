using HFP.Domain.ValueObjects.PurchaseInvoices;
using HFP.Shared.Abstractions.Domain;

namespace HFP.Domain.Entities
{
    public class PurchaseInvoice : AggregateRoot<Guid>
    {
        public Guid? Image { get; private set; }
        public PurchaseInvoiceDate Date { get; set; }


        private readonly List<PurchaseInvoiceItem> _items = new List<PurchaseInvoiceItem>();
        public IReadOnlyCollection<PurchaseInvoiceItem> Items => _items.AsReadOnly();

        public PurchaseInvoice(Guid? image, PurchaseInvoiceDate date)
        {
            Id = Guid.NewGuid();
            Image = image;
            Date = date;
        }

        internal void Update(Guid? image, PurchaseInvoiceDate date)
        {
            Image = image;
            Date = date;
        }
    }
}
