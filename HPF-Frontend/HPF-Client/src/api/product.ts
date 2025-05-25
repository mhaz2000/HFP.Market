import { authorizedAxios } from './axios/index';
import { Product, ProductCreateData, ProductUpdateData } from '../types/product';
import { ApiResponse, DefaultParams } from '../types/api';

export const createProduct = async (productCreateData: ProductCreateData): Promise<string> => {
  const { data } = await authorizedAxios.post<string>('Products', productCreateData);
  return data;
};

export const updateProduct = async (productCreateData: ProductUpdateData): Promise<string> => {
  const { data } = await authorizedAxios.put<string>('Products', productCreateData);
  return data;
};

export const getProducts = async (params?: DefaultParams): Promise<ApiResponse<Product[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<Product[]>>('Products', {
    params
  });
  return data
}

export const getProduct = async (productId: string): Promise<Product> => {
  const { data } = await authorizedAxios.get<ApiResponse<Product>>(`Products/${productId}`);
  return data.data;
};

export const deleteProduct = async (productId: string): Promise<void> => {
  await authorizedAxios.delete(`Products/${productId}`);
};

export const downloadProductsExcel = async (params?: DefaultParams): Promise<void> => {
  const response = await authorizedAxios.get<Blob>('Products/excel', {
    params,
    responseType: 'blob'
  });

  const blob = new Blob([response.data], {
    type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  });

  const url = window.URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', 'محصولات.xlsx');
  document.body.appendChild(link);
  link.click();
  link.remove();
  window.URL.revokeObjectURL(url); // Clean up the object URL
};
