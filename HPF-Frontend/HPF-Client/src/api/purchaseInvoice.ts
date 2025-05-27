import { authorizedAxios } from './axios/index';
import { ApiResponse, DefaultParams } from '../types/api';
import { PurchaseInvoice, PurchaseInvoiceTable } from '../types/purchaseInvoice';

export const createPurchaseInvoice = async (purchaseInvoiceData: PurchaseInvoice): Promise<string> => {
  const { data } = await authorizedAxios.post<string>('PurchaseInvoices', purchaseInvoiceData);
  return data;
};

export const updatePurchaseInvoice = async (purchaseInvoiceData: PurchaseInvoice): Promise<string> => {
  const { data } = await authorizedAxios.put<string>('PurchaseInvoices', purchaseInvoiceData);
  return data;
};

export const getPurchaseInvoices = async (params?: DefaultParams): Promise<ApiResponse<PurchaseInvoiceTable[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<PurchaseInvoiceTable[]>>('PurchaseInvoices', {
    params
  });
  return data
}

export const getPurchaseInvoice = async (purchaseInvoiceId: string): Promise<PurchaseInvoice> => {
  const { data } = await authorizedAxios.get<ApiResponse<PurchaseInvoice>>(`PurchaseInvoices/${purchaseInvoiceId}`);
  return data.data;
};

export const deletePurchaseInvoice = async (purchaseInvoiceId: string): Promise<void> => {
  await authorizedAxios.delete(`PurchaseInvoices/${purchaseInvoiceId}`);
};

// export const downloadPurchaseInvoicesExcel = async (params?: DefaultParams): Promise<void> => {
//   const response = await authorizedAxios.get<Blob>('PurchaseInvoices/excel', {
//     params,
//     responseType: 'blob'
//   });

//   const blob = new Blob([response.data], {
//     type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
//   });

//   const url = window.URL.createObjectURL(blob);
//   const link = document.createElement('a');
//   link.href = url;
//   link.setAttribute('download', 'محصولات.xlsx');
//   document.body.appendChild(link);
//   link.click();
//   link.remove();
//   window.URL.revokeObjectURL(url); // Clean up the object URL
// };
