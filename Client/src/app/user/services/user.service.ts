import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import StaticDetails from 'src/Data/StaticDetails';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private  http:HttpClient) { }

  getMenu(){
    return this.http.get(`${StaticDetails.API_URL}/Food/Menu`);
  }

  addToCart(data:any){
    return this.http.post(`${StaticDetails.API_URL}/FoodOrder/PlaceToCart`,data)
  }

  getFood(id:number){
    return this.http.get(`${StaticDetails.API_URL}/Food/GetFood/${id}`)
  }

  getCart(){
    return this.http.get(`${StaticDetails.API_URL}/FoodOrder/GetCart`);
  }

}
