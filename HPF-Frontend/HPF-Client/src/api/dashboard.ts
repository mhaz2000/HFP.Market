import { ApiResponse } from "../types/api";
import { DashboardData } from "../types/dashboard";
import { authorizedAxios } from "./axios";

export const getDashboardData = async (): Promise<DashboardData> => {
    const { data } = await authorizedAxios.get<ApiResponse<DashboardData>>('Dashboard');
  
    return data.data
  }