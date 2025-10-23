using HFP.Domain.ValueObjects.Base;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Domain.ValueObjects.PurchaseInvoices
{
    public class PurchaseInvoiceDate : ValueObject
    {
        public DateTime Value { get; }

        private PurchaseInvoiceDate() { } // Required for EF Core

        public PurchaseInvoiceDate(DateTime value)
        {
            if (value.Date > DateTime.UtcNow.Date.Date)
                throw new BusinessException("تاریخ فاکتور نمی‌تواند جلو تر از تاریخ حال حاضر باشد.");

            Value = value;
        }

        public static PurchaseInvoiceDate Create(DateTime value) => new PurchaseInvoiceDate(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator DateTime(PurchaseInvoiceDate purchaseInvoiceDate)
            => purchaseInvoiceDate.Value;

        public static implicit operator PurchaseInvoiceDate(DateTime purchaseInvoiceDate)
            => new(purchaseInvoiceDate);
    }
}
