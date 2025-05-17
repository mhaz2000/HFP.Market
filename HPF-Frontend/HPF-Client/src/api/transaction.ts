import { ApiResponse } from "../types/api";
import { InvoiceItem } from "../types/invoice";
import { anonymousAxios } from "./axios";

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