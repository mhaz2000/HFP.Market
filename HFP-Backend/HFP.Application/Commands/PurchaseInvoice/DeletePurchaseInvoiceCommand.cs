using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.PurchaseInvoice
{
    public record DeletePurchaseInvoiceCommand(Guid Id) : ICommand;
    
}
