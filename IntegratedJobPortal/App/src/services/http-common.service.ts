import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiResponse } from 'src/models/ApiResponse';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class HttpCommonService {

  constructor(private httpClient: HttpClient, private authenticationService: AuthenticationService) { }

  private url: string = environment.baseUrl;

  public Get(path:string): Observable<ApiResponse> {
    let headers = this.authenticationService.getTokenHeaders();
    return this.httpClient.get<ApiResponse>(this.url +path, { headers: headers });
  }

  public Post(path:string,prams:any): Observable<any> {
    let headers = this.authenticationService.getTokenHeaders();
    return this.httpClient.post(this.url +path, prams, { headers: headers })
      .pipe(map(data => {
        return data;
      }));
  }
}
