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