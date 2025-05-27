using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.PurchaseInvoice
{
    public record UpdatePurchaseInvoiceCommand(Guid Id, Guid ImageId, string Date, IEnumerable<CreatePurchaseInvoiceItemCommand> Items) : ICommand;
    
}
