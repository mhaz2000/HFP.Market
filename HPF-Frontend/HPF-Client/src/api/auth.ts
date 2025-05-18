import { anonymousAxios, authorizedAxios } from './axios/index';
import { LoginRequest } from '../types/auth'; // Import types
import { ApiResponse } from '../types/api';

export const loginUser = async (credentials: LoginRequest): Promise<string> => {
  const { data } = await anonymousAxios.post<ApiResponse<string>>('Authentication/login', credentials);
  return data.data;
};

export const checkUserState = async (): Promise<boolean> => {
  try {
    const { data } = await authorizedAxios.get<string>('Authentication/State');
    return data === 'Authenticated';
  } catch (error: any) {
    if (error.response?.status === 401) {
      return false;
    }
    throw error;
  }
};
