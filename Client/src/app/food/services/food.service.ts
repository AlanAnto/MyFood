import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import StaticDetails from 'src/Data/StaticDetails';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  constructor(private http:HttpClient) { }

  getMenu(){
    return this.http.get(`${StaticDetails.API_URL}/Food/Menu`);
  }

  foodSearch(data:any){
    return this.http.get(`${StaticDetails.API_URL}/Food/${data}`);
  }
}
