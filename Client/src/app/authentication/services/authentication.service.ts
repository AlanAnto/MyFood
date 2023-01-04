import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import StaticDetails from 'src/Data/StaticDetails';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

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
