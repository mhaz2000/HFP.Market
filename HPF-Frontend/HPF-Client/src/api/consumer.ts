import { authorizedAxios } from './axios/index';
import { ApiResponse, DefaultParams } from '../types/api';
import { Consumer, ConsumerEntry } from '../types/consumer';

export const createConsumer = async (consumerEntry: ConsumerEntry): Promise<string> => {
  const { data } = await authorizedAxios.post<string>('Consumers', consumerEntry);
  return data;
};

export const getConsumers = async (params?: DefaultParams): Promise<ApiResponse<Consumer[]>> => {
  const { data } = await authorizedAxios.get<ApiResponse<Consumer[]>>('Consumers', {
    params
  });
  return data
}

export const deleteConsumer = async (consumerId: string): Promise<void> => {
  await authorizedAxios.delete(`Consumers/${consumerId}`);
};

