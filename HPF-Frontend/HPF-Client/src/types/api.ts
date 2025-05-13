// api.ts

export type ApiErrorResponse = {
    message: string;
    code: number;
  };
  
  export type ApiResponse<T> = {
    data: T;
    totalCount?: number
  };
  
  
  export interface DefaultParams {
    pageSize?: number
    pageIndex?: number
    search?: string
    sortBy?: string
  }