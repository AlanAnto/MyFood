import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";

export class JwtHttpInterceptor implements HttpInterceptor{
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        var token = localStorage.getItem('token');
        if(token){
            let reqCopy = req.clone({
                setHeaders: {
                    'Authorization': `Bearer ${token}`,
                }
            });
            return next.handle(reqCopy);
        }
        return next.handle(req);
    }
}