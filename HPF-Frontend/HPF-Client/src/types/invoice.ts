
export type InvoiceItem = {
    price: number;
    quantity: number;
    productName: string;
    productImage: string;
    productId: string;
}

export type Transaction = {
    price: number;
    dateTime: string;
    buyerId: string;
    transactionId: string;
}