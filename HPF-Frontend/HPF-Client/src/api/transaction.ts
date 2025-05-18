import { ApiResponse, DefaultParams } from "../types/api";
import { InvoiceItem, Transaction } from "../types/invoice";
import { anonymousAxios, authorizedAxios } from "./axios";

export const getInvoice = async (buyerId: string): Promise<InvoiceItem[]> => {
  const { data } = await anonymousAxios.get<ApiResponse<InvoiceItem[]>>(`Purchase/${buyerId}`);
  return data.data;
};

export const addProductToInvoice = async ({ buyerId, productId }: { buyerId: string; productId: string }): Promise<string> => {
  const { data } = await anonymousAxios.post<string>(`Purchase`, { productId, buyerId });
  return data;
};

export const removeProductFromInvoice = async ({ buyerId, productId }: { buyerId: string; productId: string }): Promise<string> => {
  const { data } = await anonymousAxios.put<string>(`Purchase`, { productId, buyerId });
  return data;
};

export const getTransactions = async (params?: DefaultParams): Promise<ApiResponse<Transaction[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<Transaction[]>>('Transactions', {
    params
  });
  return data
}

export const getTransaction = async (transactionId: string): Promise<InvoiceItem[]> => {
  const { data } = await authorizedAxios.get<ApiResponse<InvoiceItem[]>>(`Transactions/${transactionId}`);
  return data.data;
};
