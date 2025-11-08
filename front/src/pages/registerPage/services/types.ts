export interface ServiceResponse<T = any> {
  message: string;
  isSuccess: boolean;
  httpStatusCode?: number;
  data?: T;
}