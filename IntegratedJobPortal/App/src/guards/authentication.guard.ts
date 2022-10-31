import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let isAuthorized: boolean = false;
    let requestedPageState: string = state.url;

    if (localStorage.getItem('currentUser')) {
      // logged in so return true
      var currentUser = JSON.parse(localStorage.getItem('currentUser') || '{}');
      let userName = currentUser && currentUser.userName;
      if (userName != null && userName != "")
        isAuthorized = true;
    }
    if (!isAuthorized)
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return isAuthorized;
  }

}
