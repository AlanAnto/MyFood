import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { AuthenticationService } from "src/app/authentication/services/authentication.service";

export class UserRoleGuard implements CanActivate {
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
      else if (role != 'User') {
        this.router.navigate(['/']);
        return false;
      }
  
      return true;
    }
  }

function jwt_decode(token: any): any {
  throw new Error("Function not implemented.");
}
