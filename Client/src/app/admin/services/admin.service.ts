import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import StaticDetails from 'src/Data/StaticDetails';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  getProfile(){
    return this.http.get(`${StaticDetails.API_URL}/Account/GetUser`);
  }

  editProfile(data:any)
  {
    return this.http.put(`${StaticDetails.API_URL}/Account/Update`,data);
  }

  getUsers(){
    return this.http.get(`${StaticDetails.API_URL}/Admin/ViewUsers`);
  }

  getMenu(){
    return this.http.get(`${StaticDetails.API_URL}/Food/Menu`);
  }

  addFood(data:any){
    return this.http.post(`${StaticDetails.API_URL}/Food/AddFood`,data);
  }

  addLocation(data:any){
    return this.http.post(`${StaticDetails.API_URL}/Admin/${data}`,data);
  }
}