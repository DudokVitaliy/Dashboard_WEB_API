import type HttpStatusCode from "./HttpStatusCode";

export interface ServiceResponse<T> {
    message: string;
    isSuccess: boolean;
    data: T | null;
    httpStatusCode: HttpStatusCode;
}