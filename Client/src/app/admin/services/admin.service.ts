import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import StaticDetails from 'src/Data/StaticDetails';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  editProfile(data:any)
  {
    return this.http.put(`${StaticDetails.API_URL}/Account/Update`,data);
  }
}
