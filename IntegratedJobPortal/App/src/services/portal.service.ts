import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from 'src/models/ApiResponse';
import { JobPortalEntity, PortalEntity } from 'src/models/Portal';
import { HttpCommonService } from './http-common.service';

@Injectable({
  providedIn: 'root'
})
export class PortalService {
  constructor(private httpClient: HttpCommonService) { }

  private url: string = environment.baseUrl;

  public getPortalsList(): Observable<ApiResponse> {
    return this.httpClient.Get('ijp/getportals');
  }

  public getPortal(PortalId: number): Observable<ApiResponse> {
    return this.httpClient.Get('ijp/getportal/' + PortalId);
  }

  public addPortal(portal: PortalEntity): Observable<ApiResponse> {
    return this.httpClient.Post('ijp/addportal/',portal);
  }

  public updatePortal(portal: PortalEntity): Observable<ApiResponse> {
    return this.httpClient.Post('ijp/updateportal/',portal);
  }

  public getJobPortalsList(): Observable<ApiResponse> {
    return this.httpClient.Get('ijp/getjobportals');
  }

  public getJobPortal(PortalId: number): Observable<ApiResponse> {
    return this.httpClient.Get('ijp/getjobportal/' + PortalId);
  }

  public addJobPortal(portal: JobPortalEntity): Observable<ApiResponse> {
    return this.httpClient.Post('ijp/addjobportal/',portal);
  }

  public updateJobPortal(portal: JobPortalEntity): Observable<ApiResponse> {
    return this.httpClient.Post('ijp/updatejobportal/',portal);
  }
}
