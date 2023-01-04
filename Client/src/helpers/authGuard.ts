import {Injectable} from '@angular/core';
import {CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {JwtHelperService} from "@auth0/angular-jwt";
import { AuthenticationService } from 'src/app/authentication/services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authenticationService: AuthenticationService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRole = route.data['expectedRole'];
    const user = localStorage.getItem('role');

    const helper = new JwtHelperService();
    const token: any = localStorage.getItem('token');
    const isExpired = helper.isTokenExpired(token);


    if (!user || user !== expectedRole) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
 