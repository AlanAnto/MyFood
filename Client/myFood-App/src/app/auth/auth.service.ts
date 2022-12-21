import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import StaticDetails from '../Data/StaticDetails';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  
  login(data:any)
  {
    return this.http.post(`${StaticDetails.API_URL}/Account/Login`,data);
  }

  register(data:any)
  {
    return this.http.post(`${StaticDetails.API_URL}/Account/Register`,data);
  }
}
