namespace HFP.Infrastructure.EF.Models
{
    internal class PurchaseInvoiceReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public Guid? Image { get; set; }
        public ICollection<PurchaseInvoiceItemReadModel> Items { get; set; }

    }
}
