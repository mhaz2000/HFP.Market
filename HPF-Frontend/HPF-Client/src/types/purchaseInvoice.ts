export interface PurchaseInvoice {
    id?: string;
    imageId?: string;
    date: string;
    items: PurchaseInvoiceItem[]
}

export interface PurchaseInvoiceTable {
    id: string;
    date: string;
    price: number
}

export interface PurchaseInvoiceItem {
    productName: string;
    purchasePrice: number;
    quantity: number;
}
