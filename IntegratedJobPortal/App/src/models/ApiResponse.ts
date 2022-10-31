export class ApiResponse {
    StatusCode: CustomStatusCode;
    Message: string;
    Data: any;
}

export enum CustomStatusCode {
    Success = 1,
    Error = 0
}

