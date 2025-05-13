import { anonymousAxios, authorizedAxios } from './axios/index';
import { LoginRequest } from '../types/auth'; // Import types
import { ApiResponse } from '../types/api';

export const loginUser = async (credentials: LoginRequest): Promise<string> => {
  const { data } = await anonymousAxios.post<ApiResponse<string>>('Authentication/login', credentials);
  return data.data;
};

export const checkUserState = async (): Promise<string> => {
  const { data } = await authorizedAxios.get<string>('Authentication/State');
  return data;
};