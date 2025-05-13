import { ApiResponse } from "../types/api";
import { InvoiceItem } from "../types/invoice";
import { anonymousAxios } from "./axios";

export const getInvoice = async (buyerId: string): Promise<InvoiceItem[]> => {
  const { data } = await anonymousAxios.get<ApiResponse<InvoiceItem[]>>(`Purchase/${buyerId}`);
  return data.data;
};