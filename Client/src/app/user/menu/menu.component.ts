import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  menu : any;
  food = {
    id:0,
    quantity:0,
    price:0.0
  }
  constructor (private userService:UserService){}
  
  ngOnInit(){
    this.userService.getMenu().subscribe((res:any)=>{
      console.log(res);
      this.menu = res.data;
    })
  }  

  handleAddToCart(id:number,quantity:any,price:number){
    console.log(quantity);
    this.food.id = id;
    // this.food.quantity = quantity;
    this.food.price = price;
    // this.userService.addToCart(this.food).subscribe(res =>{
    //   console.log(res);
    // })
  }
}
