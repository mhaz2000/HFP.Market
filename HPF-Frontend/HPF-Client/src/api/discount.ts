import { authorizedAxios } from './axios/index';
import { Discount, CreateDiscount, UpdateDiscount } from '../types/discount';
import { ApiResponse, DefaultParams } from '../types/api';

export const createDiscount = async (discountCreateData: CreateDiscount): Promise<string> => {
  const { data } = await authorizedAxios.post<string>('Discounts', discountCreateData);
  return data;
};

export const updateDiscount = async (discountCreateData: UpdateDiscount): Promise<string> => {
  const { data } = await authorizedAxios.put<string>('Discounts', discountCreateData);
  return data;
};

export const getDiscounts = async (params?: DefaultParams): Promise<ApiResponse<Discount[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<Discount[]>>('Discounts', {
    params
  });
  return data
}

export const getDiscount = async (discountId: string): Promise<Discount> => {
  const { data } = await authorizedAxios.get<ApiResponse<Discount>>(`Discounts/${discountId}`);
  return data.data;
};

export const deleteDiscount = async (discountId: string): Promise<void> => {
  await authorizedAxios.delete(`Discounts/${discountId}`);
};