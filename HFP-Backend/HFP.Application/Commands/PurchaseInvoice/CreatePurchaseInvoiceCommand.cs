using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.PurchaseInvoice
{
    public record CreatePurchaseInvoiceCommand(Guid? ImageId, DateTime Date, IEnumerable<CreatePurchaseInvoiceItemCommand> Items) : ICommand;
    
}
