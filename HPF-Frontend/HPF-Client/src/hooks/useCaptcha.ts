// src/hooks/useCaptcha.ts
import { useQuery } from '@tanstack/react-query';
import { anonymousAxios } from '../api/axios/index';
import { CaptchaResponse } from '../types/captcha';
import { ApiResponse } from '../types/api';

const fetchCaptcha = async (): Promise<CaptchaResponse> => {
  const { data } = await anonymousAxios.get<ApiResponse<CaptchaResponse>>('/Captcha');
  return data.data;
};

export const useCaptcha = () => {
  return useQuery({
    queryKey: ['captcha'],
    queryFn: fetchCaptcha,
    refetchOnWindowFocus: false,
  });
};
