import { authorizedAxios } from './axios/index';
import { ApiResponse, DefaultParams } from '../types/api';
import { Warehosueman, WarehosuemanEntry } from '../types/warehouseman';

export const createWarehouseman = async (warehosuemanEntry: WarehosuemanEntry): Promise<string> => {
  const { data } = await authorizedAxios.post<string>('Warehousemen', warehosuemanEntry);
  return data;
};

export const getWarehousemen = async (params?: DefaultParams): Promise<ApiResponse<Warehosueman[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<Warehosueman[]>>('Warehousemen', {
    params
  });
  return data
}

export const deleteWarehouseman = async (warehousemanId: string): Promise<void> => {
  await authorizedAxios.delete(`Warehousemen/${warehousemanId}`);
};

