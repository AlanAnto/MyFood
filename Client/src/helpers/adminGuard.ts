import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from "@auth0/angular-jwt";
import { AuthenticationService } from 'src/app/authentication/services/authentication.service';
import jwt_decode from "jwt-decode"

@Injectable({
  providedIn: 'root'
})
export class AdminRoleGuard implements CanActivate {
  constructor(private authenticationService: AuthenticationService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    const helper = new JwtHelperService();
    const token: any = localStorage.getItem('token');
    const parsedToken : any = jwt_decode(token);
    //const parsedToken = helper.decodeToken(token);
    const isExpired = helper.isTokenExpired(token);
    const role = parsedToken.Role;

    if (!token || isExpired) {
      this.router.navigate(['authentication/login']);
      return false;
    }
    else if (role != 'Admin') {
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}

export default AdminRoleGuard