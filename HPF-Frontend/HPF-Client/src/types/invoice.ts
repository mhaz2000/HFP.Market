import { DefaultParams } from "./api";

export type InvoiceItem = {
    price: number;
    quantity: number;
    productName: string;
    productImage: string;
    productId: string;
    productCode: string;
}

export type Transaction = {
    price: number;
    dateTime: string;
    buyerId: string;
    transactionId: string;
}

export type ProfitReportData = {
    productName: string;
    availableQuantity: number;
    soldQuantity: number;
    profit: number;
}


export interface TransactionFilter extends DefaultParams {
    startDate?: string
    endDate?: string
}