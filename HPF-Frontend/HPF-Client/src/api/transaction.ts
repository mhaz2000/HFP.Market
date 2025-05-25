import { ApiResponse, DefaultParams } from "../types/api";
import { InvoiceItem, ProfitReportData, Transaction, TransactionFilter } from "../types/invoice";
import { anonymousAxios, authorizedAxios } from "./axios";

export const getInvoice = async (buyerId: string): Promise<InvoiceItem[]> => {
  const { data } = await anonymousAxios.get<ApiResponse<InvoiceItem[]>>(`Purchase/${buyerId}`);
  return data.data;
};

export const addProductToInvoice = async ({ buyerId, productCode }: { buyerId: string; productCode: string }): Promise<string> => {
  const { data } = await anonymousAxios.post<string>(`Purchase`, { productCode, buyerId });
  return data;
};

export const removeProductFromInvoice = async ({ buyerId, productId }: { buyerId: string; productId: string }): Promise<string> => {
  const { data } = await anonymousAxios.put<string>(`Purchase`, { productId, buyerId });
  return data;
};

export const getTransactions = async (params?: TransactionFilter): Promise<ApiResponse<Transaction[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<Transaction[]>>('Transactions', {
    params
  });
  return data
}

export const getProfitReport = async (params?: DefaultParams): Promise<ApiResponse<ProfitReportData[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<ProfitReportData[]>>('Transactions/ProfitReport', {
    params
  });
  return data
}

export const getTransaction = async (transactionId: string): Promise<InvoiceItem[]> => {
  const { data } = await authorizedAxios.get<ApiResponse<InvoiceItem[]>>(`Transactions/${transactionId}`);
  return data.data;
};

export const downloadTransactionsExcel = async (params?: DefaultParams): Promise<void> => {
  const response = await authorizedAxios.get<Blob>('Transactions/excel', {
    params,
    responseType: 'blob'
  });

  const blob = new Blob([response.data], {
    type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  });

  const url = window.URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', 'تراکنش‌ها.xlsx');
  document.body.appendChild(link);
  link.click();
  link.remove();
  window.URL.revokeObjectURL(url); // Clean up the object URL
};

export const downloadProfitReportExcel = async (params?: DefaultParams): Promise<void> => {
  const response = await authorizedAxios.get<Blob>('Transactions/ProfitReportExcel', {
    params,
    responseType: 'blob'
  });

  const blob = new Blob([response.data], {
    type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  });

  const url = window.URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', 'گزارش فروش.xlsx');
  document.body.appendChild(link);
  link.click();
  link.remove();
  window.URL.revokeObjectURL(url); // Clean up the object URL
};