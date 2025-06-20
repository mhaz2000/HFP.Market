﻿using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.PurchaseInvoice
{
    public record CreatePurchaseInvoiceItemCommand(string ProductName, int Quantity, decimal PurchasePrice);
    
}
