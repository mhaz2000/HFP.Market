export interface PurchaseInvoice {
    id?: string;
    imageId?: string;
    date: string;
}


export interface PurchaseInvoiceItem {
    name: string;
    price: number;
    quantity: number;
}
