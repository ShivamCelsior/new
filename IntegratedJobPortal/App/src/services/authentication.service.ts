import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

export interface LoginResponse {
    UserName: string;
    CandidateId: any;
    AadhaarNumber: string;
    Mobile: string;
    Email: string;
    ProfileId: any;
    ProfileStatusId: any;
    ProfileStatus: string;
    MRFId: any;
    OfferId: any;
    RecruiterId: any;
    RecruiterName: string;
    RecruiterEmail: string;
    RecruiterContact: string;
    RecruiterPSTN: string;
    RecruiterVOIP: string;
    AuthToken: string;
    IdleTimeoutMins: string;
    ApplicationVersion: string;
}

@Injectable({
    providedIn: 'root'
})

export class AuthenticationService {

    baseUrl = environment.baseUrl;
    public token: string;
    public email: string;
    public roles: { RoleId: number, RoleName: string }[];
    public username: string;
    public userEmail: string;
    public TokenId: string;
    public groupId: string;
    public userId: string;
    public pCoreUserId: number;
    // public employeeIdForConsultant: string;

    constructor(private http: HttpClient) {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser') || '{}');

        this.token = currentUser && currentUser.token;
        this.roles = currentUser && currentUser.roles;

        this.username = currentUser && currentUser.username;
        this.userEmail = currentUser && currentUser.userEmail;
        this.groupId = currentUser && currentUser.groupId;
        this.userId = currentUser && currentUser.userId;
        this.pCoreUserId = currentUser && currentUser.pCoreUserId;
    }
    //get current authenticated user from local storage.
    getCurrentAuthenticatedUser() {
        var currentUser = JSON.parse(localStorage.getItem('currentUser') || '{}');
        return currentUser;
    }

    //get current authenticated consultant from local storage.
    getCurrentAuthenticatedCandidate() {
        return JSON.parse(localStorage.getItem('currentcandidate') || '{}');
    }




    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = '';
        localStorage.removeItem('currentUser');
    }





    getTokenHeaders(): HttpHeaders {
        // add authorization header with jwt token
        let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
        headers.append('TokenHeader', this.token);
        return headers;
    }

    getCandidateTokenHeaders(): HttpHeaders {
        return new HttpHeaders({
            'Content-Type': 'application/json',
            'TokenHeader': this.getToken()
        });
    }
    //returns consultant token ID
    getToken(): string {
        var TokenInfo = JSON.parse(localStorage.getItem('currentUser') || '{}');
        this.TokenId = TokenInfo && TokenInfo.token;
        return this.TokenId;
    }
    getUserId(): string {
        var UserInfo = JSON.parse(localStorage.getItem('currentUser') || '{}');
        this.userId = UserInfo && UserInfo.userId;
        return this.userId;
    }
    getPCoreUserId(): string {
        var UserInfo = JSON.parse(localStorage.getItem('currentUser') || '{}');
        this.pCoreUserId = UserInfo && UserInfo.pCoreUserId;
        return this.userId;
    }

    getUserName(): string {
        var UserInfo = JSON.parse(localStorage.getItem('currentUser') || '{}');
        this.username = UserInfo && UserInfo.userName;
        return this.username;
    }

    //verify user on the basis of username and password
    login(username: string, password: string): Observable<any> {
        return this.http.post<any>(this.baseUrl + 'ijp/UserLogin', { username: username, password: password })
            .pipe(map(response => {
                // login successful if there's a jwt token in the response

                let token = response && response.AuthToken;
                if (token) {
                    // set token property
                    this.token = token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({
                        userName: response.UserName,
                        userId: response.UserId,
                        token: token,
                        groupId: response.GroupId,
                        userEmail: response.Email,
                        crintellPassword: response.CrintellPassword,
                        pCoreUserId: response.PCoreUserId,

                    }));
                    localStorage.setItem('IJPPCoreUserId', response.PCoreUserId);
                    localStorage.setItem('IJPPCoreUserId', response.PCoreUserId);
                    localStorage.setItem('IJPUserId', response.UserId);
                    localStorage.setItem('IJPUserName', response.UserName);
                    /* chrome.storage.sync.set({ "IJPUserId": response.UserId }, function(){
                       //  A data saved callback omg so fancy
                   });
                  chrome.storage.sync.set({ "IJPUserName": response.UserName }, function(){
                       //  A data saved callback omg so fancy
                   });  */

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            }));
    }

    forgotPassword(loginId: any) {
        let headers = this.getTokenHeaders();
        let info = {
            LoginId: loginId
        }
        return this.http.post(this.baseUrl + 'ijp/forgotPassword', info)
            .pipe(map(response => {
                let result = response;
                return result;
            }));
    }

    resetPassword(loginId: any, securityCode: any, newPassword: any) {
        let headers = this.getTokenHeaders();
        let info = {
            LoginId: loginId,
            SecurityCode: securityCode,
            Password: newPassword
        }
        return this.http.post(this.baseUrl + 'ijp/resetPassword', info)
            .pipe(map((response: any) => {
                let result = response;
                return result;
            }));
    }

    isUserActive(userId: any) {
        let headers = this.getTokenHeaders();
        /*  let info={
             UserId:userId,
             IPAddress:ipAddress
           } */
        return this.http.get(this.baseUrl + 'ijp/isUserActive/' + userId)
            .pipe(map((response: any) => {
                let result = response;
                return result;
            }));
    }
}
